﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Intefaces;

namespace Task_3
{
    public class Phone
    {
        public event EventHandler<CallEventArgs> OutgoingCall;
        public event EventHandler<CallEventArgs> IncomingCall;
        public event EventHandler AcceptCall;
        public event EventHandler RejectCall;
        public event EventHandler<IPort> ConnectingToPort;

        public void Call(CallEventArgs args)
        {
            Console.WriteLine("\nтел1 начинает звонок\n");
            OnOutgoingCall(this, new CallEventArgs() {SourcePhoneNumber = "111-11-11", TargetPhoneNumber = args.TargetPhoneNumber });
        }

        protected virtual void OnOutgoingCall(object sender, CallEventArgs args)
        {
            OutgoingCall?.Invoke(sender, args);
        }

        public void OnIncomingCall(object sender, CallEventArgs args)
        {
            // состояние и не забыть выйти (вернуть управление)
            Console.WriteLine($"{args.SourcePhoneNumber} дозвонился до второго пользователя");
        }

        // два варианта либо в какой-то метод передавать реакцию пользователя, либо делать два метода Accept & Reject
        public void Accept()
        {
            // дальше этот метод должен бежать по другой цепочке событий, добежать до станции, станция проверяет и если да (accept) меняет свое состояние (н.п. из состояния ожидания в состояние 
            // соединения, а потом станция генерирует событие для биллинговой системы.
        }
    }
}