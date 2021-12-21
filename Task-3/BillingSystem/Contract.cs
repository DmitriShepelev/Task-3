using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.BillingSystem.Intefaces;

namespace Task_3.BillingSystem
{
    public class Contract
    {
        public string FirstName { get; }
        public string LastName { get; }
        public ITariffPlan Tariff { get; }

        public Contract(string firstName, string lastName, ITariffPlan tariff)
        {
            FirstName = firstName;
            LastName = lastName;
            Tariff = tariff;
        }
        


    }
}
