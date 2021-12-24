using System;
using System.Collections.Generic;
using System.Linq;
using Task_3.BillingSystem;
using Task_3.DateBase;

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
            //p1.OutgoingCall += port1.OnPortOutgoingCall;
            //port1.OutgoingCall += station.OnPortOutgoingCall;

            //p2.OutgoingCall += port2.OnPortOutgoingCall;
            //port2.OutgoingCall += station.OnPortOutgoingCall;

            //port1.IncomingCall += p1.OnIncomingCall;
            //port2.IncomingCall += p2.OnIncomingCall;

            ////generate call
            //p1.Call(new CallEventArgs() { TargetPhoneNumber = "222-22-22" });


            //var contract1 = new Contract("Dmitri", "Shepelev", new TariffPlanLight());
            //var contract2 = new Contract("Arkadiy", "Dobkin", new TariffPlanLight());

            //var subscriber1 = new Subscriber(contract1, "752-69-16");
            //var subscriber2 = new Subscriber(contract2, "111-11-11");

            //var phone1 = subscriber1.Phone;
            //var phone2 = subscriber2.Phone;
            //phone1.ConnectPort();
            //phone2.ConnectPort();

            ////Station station = new();

            ////phone1.Port.OutgoingCall += Station.OnPortOutgoingCall;
            ////phone1.Port.IncomingCall += phone1.OnIncomingCall;


            ////phone2.Port.IncomingCall += phone2.OnIncomingCall;


            //phone1.Call("111-11-11");

            var db = new DataBase();
            db.Calls.Add(new Call() {
                StartCall = DateTime.Now,
                EndCall = DateTime.Now + TimeSpan.FromSeconds(3)
            });

            var query = db.Contracts.Where(x => x.Id == 4812853).Join(db.Calls, ct => ct.Id, cl => cl.ContractId,
                (contract, call) => call.CallCost);
        }

    }
}
