using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.ATS;
using Task_3.DateBase;

namespace Task_3.BillingSystem
{
    public class Subscriber 
    {
        public PortController Controller { get; }

        public Subscriber(PortController controller)
        {
            Controller = controller;
        }
        public Contract Contract { get; }
        public Phone Phone { get; }

        public Subscriber(Contract contract, string phoneNumber)
        {
            Contract = contract;
            Phone = new Phone(phoneNumber, new Station(Controller));
            Controller.AddMatchPair(Phone.PhoneNumber, Phone.Port);

        }
    }
}
