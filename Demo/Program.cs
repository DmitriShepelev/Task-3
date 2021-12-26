using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Task_3.ATS;
using Task_3.MobileOperator;
using Task_3.DateBase;

namespace Demo
{
    public class Program
    {
        public static void Main()
        {
            var op = new MobileOperator();

            var t = new TariffLight();

            var c1 = new Client("Dmitri", "Shepelev");
            var c2 = new Client("Piers", "Orson");
            var c3 = new Client("Christen", "Len");

            var ph1 = op.SignContract(c1, t, "752-69-16");
            var ph2 = op.SignContract(c2, t, "222-22-22");
            var ph3 = op.SignContract(c3, t, "333-33-333");

            op.ChangeTariff(c1, new TariffMax());
            ph1.DisconnectPort();
            ph1.ConnectPort();


            for (var i = 0; i < 3; i++)
            {
                CallAcceptEmulator(ph1, ph2);
                CallAcceptEmulator(ph1, ph3);
                CallAcceptEmulator(ph2, ph1);
                CallRejectEmulator(ph2, ph3);
                CallRejectEmulator(ph3, ph1);
            }

            var detailedCallList = op.GetDetailedReport(ph1, 5);
            Console.WriteLine($"\nDetailed report for number \"{ph1.PhoneNumber}\"");
            Print(detailedCallList);


            var reportCallList = op.GetReportByDate(ph2, DateTime.Today).ToArray();
            if (!reportCallList.Any()) Console.WriteLine("No calls for the selected date.");
            Console.WriteLine($"Date report for number \"{ph2.PhoneNumber}\"");
            Print(reportCallList);

            var callListByCost = op.GetReportByCost(ph1, 0.001m, 0.30m);
            Console.WriteLine($"Report for number \"{ph1.PhoneNumber}\" filtered by a given cost:");
            Print(callListByCost);

            var callListBySpecificSubscriber = op.GetReportFilteredBySpecificSubscriber(ph1, ph2);
            Console.WriteLine($"Report for \"{ph1.PhoneNumber}\" filtered by \"{ph2.PhoneNumber}\"");
            Print(callListBySpecificSubscriber);
        }

        private static void CallAcceptEmulator(Phone phone1, Phone phone2)
        {
            var random = new Random();
            phone1.Call(phone2.PhoneNumber);
            phone2.Accept();
            Thread.Sleep(TimeSpan.FromSeconds(random.Next(1, 3)));
            phone2.HangUp();
        }
        private static void CallRejectEmulator(Phone phone1, Phone phone2)
        {
            phone1.Call(phone2.PhoneNumber);
            phone2.Reject();
        }

        private static void Print(IEnumerable<string> calls)
        {
            Console.WriteLine(new string('-', 90));
            foreach (var call in calls)
            {
                Console.WriteLine(call);
            }
            Console.WriteLine(new string('-', 90));
        }
    }
}
