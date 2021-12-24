using System;
using Task_3.MobileOperator;
using Task_3.DateBase;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var op = new MobileOperator();
            //op.DataBase.Calls.Add();
            //var x = new DataBase();
            var t = new TariffLight();
            Console.WriteLine(t.Id);
            var c1 = new Client("aaa", "bbb");
            var c2 = new Client("ccc", "ddd");
            Console.WriteLine(c1.Id + " " + c2.Id);

            var ph1 = op.SignContract(c1, t, "752-69-16");
            var ph2 = op.SignContract(c2, t, "111-22-33");

            ph1.ConnectPort();
            ph1.Call("123");
            ph1.Call("111-22-33");
            ph2.Accept();
            //ph2.Reject();
            ph2.HangUp();

            //op.Print(c1);
            //op.ChangeTariff(c1, new TariffMax());
            //Console.WriteLine();
            // op.Print(c1);
        }
    }
}
