using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class Port
    {
        public PortState State { get; set; }
        public int Id => GetHashCode();

        public event EventHandler<CallEventArgs> OutgoingCall;
        public event EventHandler<CallEventArgs> IncomingCall;

        public void OnPhoneOutgoingCall(object sender, CallEventArgs args)
        {
            Console.WriteLine($"\nстатус порта {State}\n") ;
            // поменять статус и инициировать следующее событие
            if (State == PortState.Free)
            {
                State = PortState.Busy;
                //при необходимости можно создавать новый объект и передавать ему что нужно
                Console.WriteLine($"\nпорт меняет статус на занято и порт сообщает станции об исходящем звонке\n");
                OnOutgoingCall(this, args);
            }
        }

        protected virtual void OnOutgoingCall(object sender, CallEventArgs args)
        {
            OutgoingCall?.Invoke(sender, args);
        }

        public void OnIncomingCall(object sender, CallEventArgs args)
        {
            //зажигать событие и както обрабатывать
            // как вариант: развести по интерфейсам "штуки которые звонят и штуки которые принимают"
            if (State == PortState.Free)
            {
                State = PortState.Busy;
                OnStationIncomingCall(this, args);
            }
        }

        protected virtual void OnStationIncomingCall(object sender, CallEventArgs args)
        {
            IncomingCall?.Invoke(sender, args);
        }
    }
}
