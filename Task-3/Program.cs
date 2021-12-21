using System;
using System.Collections.Generic;
using Task_3.BillingSystem;

namespace Task_3
{
    class Program
    {
        public static void Main()
        {
            ////Create objects
            //Port port1 = new();
            //Phone p1 = new("123", port1);

            //Port port2 = new();
            //Phone p2 = new("222-22-22", port2);

            //Station station = new(10);
            //PortController.AddMatchPair("123", port1);
            //PortController.AddMatchPair("222-22-", port2);

            ////bindings
            //// не обязательно здесь, может быть в разных файлах
            //p1.OutgoingCall += port1.OnPhoneOutgoingCall;
            //port1.OutgoingCall += station.OnPhoneOutgoingCall;

            //p2.OutgoingCall += port2.OnPhoneOutgoingCall;
            //port2.OutgoingCall += station.OnPhoneOutgoingCall;

            //port1.IncomingCall += p1.OnIncomingCall;
            //port2.IncomingCall += p2.OnIncomingCall;

            ////generate call
            //p1.Call(new CallEventArgs() { TargetPhoneNumber = "222-22-22" });


            var contract1 = new Contract("Dmitri", "Shepelev", new TariffPlanLight());
            var contract2 = new Contract("Arkadiy", "Dobkin", new TariffPlanLight());

            var subscriber1 = new Subscriber(contract1, "752-69-16");
            var subscriber2 = new Subscriber(contract2, "111-11-11");

            var phone1 = subscriber1.Phone;
            var phone2 = subscriber2.Phone;

            Station station = new();

            phone1.StationPort.OutgoingCall += station.OnPhoneOutgoingCall;
            phone1.StationPort.IncomingCall += phone1.OnIncomingCall;


            phone1.ConnectPort();
            phone1.Call("111-11-11");

        }

    }
}
