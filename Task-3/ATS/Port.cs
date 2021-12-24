using System;
using Task_3.ATS.States;

namespace Task_3.ATS
{
    public class Port
    {
        public PortState State { get; set; }
        public bool IsUsed { get; set; }
        public int Id => this.GetHashCode();

        public event EventHandler<CallEventArgs> OutgoingCall;
        public event EventHandler<CallEventArgs> IncomingCall;
        public event EventHandler<CallEventArgs> AcceptCall;
        public event EventHandler<CallEventArgs> RejectCall;
        public event EventHandler<CallEventArgs> EndOfCall;



        public void OnPhoneOutgoingCall(object sender, CallEventArgs args)
        {
            // поменять статус и инициировать следующее событие
            if (State != PortState.Free) return;
            Console.WriteLine($"\nстатус порта {State}\n");
            State = PortState.Busy;
            //при необходимости можно создавать новый объект и передавать ему что нужно
            Console.WriteLine($"\nпорт меняет статус на занято и порт сообщает станции об исходящем звонке\n");
            this.ContinueOutgoingCall(this, args);
        }

        protected virtual void ContinueOutgoingCall(object sender, CallEventArgs args)
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
                this.ContinueIncomingCall(this, args);
            }
        }

        protected virtual void ContinueIncomingCall(object sender, CallEventArgs args)
        {
            IncomingCall?.Invoke(sender, args);
        }

        public void OnPhoneAcceptCall(object sender, CallEventArgs callEventArgs)
        {
            Console.WriteLine(State + " " + Id);
            this.ContinueAcceptCall(sender, callEventArgs);
        }

        protected virtual void ContinueAcceptCall(object sender, CallEventArgs callEventArgs)
        {
            AcceptCall?.Invoke(sender, callEventArgs);
        }

        public void OnPhoneRejectCall(object sender, CallEventArgs callEventArgs)
        {
            this.ContinueRejectCall(sender, callEventArgs);
        }

        protected virtual void ContinueRejectCall(object sender, CallEventArgs callEventArgs)
        {
            RejectCall?.Invoke(sender, callEventArgs);
        }

        public void OnPhoneEndOfCall(object sender, CallEventArgs callEventArgs)
        {
            this.ContinueEndOfCall(sender, callEventArgs);
        }

        protected virtual void ContinueEndOfCall(object sender, CallEventArgs callEventArgs)
        {
            EndOfCall?.Invoke(sender, callEventArgs);
        }
    }
}
