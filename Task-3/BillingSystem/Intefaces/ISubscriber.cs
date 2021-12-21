using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.BillingSystem.Intefaces
{
   public interface ISubscriber
    {
        public string FirstName { get; }
        public string LastName { get; }
    }
}
