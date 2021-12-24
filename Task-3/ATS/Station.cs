using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Task_3.ATS.States;
using Task_3.DateBase;
using static Task_3.ATS.PortController;

namespace Task_3.ATS
{
    public class Station
    {
        private readonly List<Port> _listPorts;
        private readonly int _totalNumberOfPorts = Convert.ToInt32
            (ConfigurationManager.AppSettings.Get("totalNumberOfPorts"));

        private readonly PortController _portController;
        private List<CallEventArgs> _callsInInitializationState = new();
        private List<CallEventArgs> _callsInDialingState = new();
        //public event EventHandler<CallEventArgs> CallRecordCreate;

        public Station(PortController portController)
        {
            _portController = portController;
            _listPorts = new List<Port>(_totalNumberOfPorts);
            this.SetListPorts();
            this.BindPorts();
        }


        public Port GetFreePort {
            get {
                var port = _listPorts.FirstOrDefault(x => x.State == PortState.Free && !x.IsUsed);
                if (port != null)
                {
                    port.IsUsed = true;
                }
                return port;
            }
        }


        public void OnPortOutgoingCall(object sender, CallEventArgs args)
        {
            Console.WriteLine($"Станция:Телефон пытается дозвонится по номеру {args.TargetPhoneNumber}");

            _callsInInitializationState.Add(args);

            var targetPort = _portController.GetPort(args.TargetPhoneNumber);
            if (targetPort != null && targetPort.State == PortState.Free)
            {
                args.CallState = CallState.Dialing;
                _portController.GetPort(args.TargetPhoneNumber)?.OnIncomingCall(sender, args);
            }
        }

        public void OnPortAcceptCall(object sender, CallEventArgs args)
        {
            var index = _callsInInitializationState.FindIndex(0, _callsInInitializationState.Count,
                i => i.Id == args.Id);
            _callsInDialingState.Add(_callsInInitializationState[index]);
            _callsInInitializationState.RemoveAt(index);
        }

        public void OnPortRejectCall(object sender, CallEventArgs args)
        {
            var index = _callsInInitializationState.FindIndex(0, _callsInInitializationState.Count,
                i => i.Id == args.Id);
            _callsInDialingState.Add(_callsInInitializationState[index]);
            _callsInInitializationState.RemoveAt(index);
        }
        public void OnPortEndOfCall(object sender, CallEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void SetListPorts()
        {
            for (var i = 0; i < _listPorts.Capacity; i++) _listPorts.Add(new Port());
        }

        private void BindPorts()
        {
            foreach (var port in _listPorts)
            {
                port.OutgoingCall += this.OnPortOutgoingCall;
                port.AcceptCall += this.OnPortAcceptCall;
                port.RejectCall += this.OnPortRejectCall;
                port.EndOfCall += this.OnPortEndOfCall;
            }
        }

    }
}
//на станции несколько контейнеров 1. те звонки, которые в состоянии инициализации (соединение еще не произошло): либо один звонит другому, либо ожидает от него ответа (срабатывает на станции на первой стадии)
//далее можно модифицировать инф объект (добавить еще каких-нибудь состояний) и вернувшись опять на станцию можем н.п. перевести звонок в состояние дозвона (телефон второго непрерывно звонит)
// далее соединение может либо произойти, либо нет. Если да, то переводим объект из первого списка во второй - звонки которые сейчас текут. Но независимо от результата дозвона, должно происходить событие, которое можно назвать CallRecordCreate (это для биллинговой системы), общение с бил сис только через это событие (инжектировать ее в станцию не надо!)


// в бил сис должен быть обработчик события (OnCallRecordCreated()). Делаем прообраз БД, но на коллекциях. Внутри может быть коллекция объектов вида "Договор", коллекция оъектов вида "Информация о совершенном звонке", можно инжентировать зависимость на объект типа "Тарифный план"
// (желательно хорошо продумать, выделить Интерфейс - их у компании может быть несколько; выделить общий механизм расчета и заложить в бил сис для конечного итога расчетов по каждому абоненту исходя из общего механизма. т.е. - заложить ТП, список звонков и эта штуковина через "что-то такое общее" должна расчитать нам итоговую сумму; должна быть возможность смены тарифного плана).
// Модель данных сразу стройте как реляционную, т.е. соответствия вида первичный\внешний ключ не через ссылки на объекты, а через идентификаторы каких-то ссущностей. МиниБД, но только на коллекциях. подумать - карта соответствия между тел-ми и портами (номера тел-в - только в биллинговой системе). создать объекты портов и заматчить их на какие-то телефоны и в контроллере предусмотреть возможность смены порта для определенного телефона. 

// Отдельно стоят этап создания объектов. Отдельно - этап привязки. привязки могут быть как изначальными, так и строится "на ходу"(н.п. - назначили телефону порт, нужно что-то убрать, что-то привязать). И уже потом  - сценарий испольнения. Accept и Reject - это костыль, но костыль необходимый, что-бы не реализовывать в виде системы реального времени.