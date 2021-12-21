using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.BillingSystem.Intefaces;

namespace Task_3.BillingSystem
{
    public class Subscriber //: ISubscriber
    {
        public Contract Contract { get; }
        public Phone Phone { get; }

        public Subscriber(Contract contract, string phoneNumber)
        {
            Contract = contract;
            Phone = new Phone(phoneNumber, Station.GetFreePort);
            PortController.AddMatchPair(Phone.PhoneNumber, Phone.StationPort);
        }
    }
}
