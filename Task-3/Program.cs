using System;
using System.Collections.Generic;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create objects
            Phone p1 = new();
            Port port1 = new();

            Phone p2 = new();
            Port port2 = new();


            Station station = new()
            {
                PortController = new PortController(new List<Port>() { port1, port2 })
            };

            //bindings
            // не обязательно здесь, может быть в разных файлах
            p1.OutgoingCall += port1.OnPhoneOutgoingCall;
            port1.OutgoingCall += station.OnPhoneOutgoingCall;

            p2.OutgoingCall += port2.OnPhoneOutgoingCall;
            port2.OutgoingCall += station.OnPhoneOutgoingCall;

            port1.IncomingCall += p1.OnIncomingCall;
            port2.IncomingCall += p2.OnIncomingCall;

            //generate call

            p1.Call(new CallEventArgs() { TargetPhoneNumber = "222-22-22" });

        }

    }
}
