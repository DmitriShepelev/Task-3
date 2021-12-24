using System;
using Task_3.ATS.States;

namespace Task_3.ATS
{
    public class Phone
    {
        public int Id => this.GetHashCode();
        public Port Port { get; }
        public string PhoneNumber { get; }
        private Station Station { get; }
        private CallEventArgs _argsIncomingCall;


        public event EventHandler<CallEventArgs> OutgoingCall;
        //public event EventHandler<CallEventArgs> IncomingCall;

        public event EventHandler<CallEventArgs> AcceptCall;
        public event EventHandler<CallEventArgs> RejectCall;
        public event EventHandler<CallEventArgs> EndOfCall;
        //public event EventHandler ConnectingToPort;

        public Phone(string number, Station station)
        {
            PhoneNumber = number;
            Station = station;
            Port = Station.GetFreePort;
        }

        public Port ConnectPort()
        {
            OutgoingCall += Port.OnPhoneOutgoingCall;
            Port.IncomingCall += this.OnIncomingCall;
            AcceptCall += Port.OnPhoneAcceptCall;
            RejectCall += Port.OnPhoneRejectCall;
            EndOfCall += Port.OnPhoneEndOfCall;
            return Port;
        }

        public void DisconnectPort()
        {
            Port.State = PortState.Free;
            OutgoingCall -= Port.OnPhoneOutgoingCall;
            Port.IncomingCall -= this.OnIncomingCall;
            Port.IsUsed = false;
        }

        public void Call(string targetPhoneNumber)
        {
            Console.WriteLine("\nтел1 начинает звонок\n");
            this.StartOutgoingCall(this,
                new CallEventArgs() { Id = this.GetHashCode(), SourcePhoneNumber = PhoneNumber, TargetPhoneNumber = targetPhoneNumber });
            Port.State = PortState.Free;
        }

        protected virtual void StartOutgoingCall(object sender, CallEventArgs args)
        {
            OutgoingCall?.Invoke(sender, args);
        }

        public void OnIncomingCall(object sender, CallEventArgs args)
        {
            // Notify user
            Console.WriteLine($"\n{args.SourcePhoneNumber} calls to {args.TargetPhoneNumber}");
            _argsIncomingCall = args;
        }

        public void Accept()
        {
            // дальше этот метод должен бежать по другой цепочке событий, добежать до станции, станция проверяет и если да (accept) меняет свое состояние (н.п. из состояния ожидания в состояние 
            // соединения, а потом станция генерирует событие для биллинговой системы.

            Console.WriteLine("Трубку подняли");
            if (_argsIncomingCall.Id != 0) this.TakeCall(this, _argsIncomingCall);

        }

        public void Reject()
        {
            if (_argsIncomingCall.Id != 0) this.CancelCall(this, _argsIncomingCall);
        }

        protected virtual void CancelCall(Phone phone, CallEventArgs argsIncomingCall)
        {
            RejectCall?.Invoke(phone, argsIncomingCall);
        }

        protected virtual void TakeCall(Phone phone, CallEventArgs callEventArgs)
        {
            AcceptCall?.Invoke(phone, callEventArgs);
        }

        public void HangUp()
        {
            if (_argsIncomingCall.Id != 0) this.GetOffThePhone(this, _argsIncomingCall);
        }

        protected virtual void GetOffThePhone(Phone phone, CallEventArgs argsIncomingCall)
        {
            EndOfCall?.Invoke(phone, argsIncomingCall);
        }
    }
}
