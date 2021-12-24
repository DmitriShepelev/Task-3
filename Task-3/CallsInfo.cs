using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.BillingSystem;

namespace Task_3
{
   public class CallsInfo
    {
        public Subscriber Subscriber { get; set; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }

        public CallsInfo(Subscriber subscriber, DateTime beginCall, DateTime endCall)
        {
            Subscriber = subscriber;
            BeginCall = beginCall;
            EndCall = endCall;
        }
    }
}
