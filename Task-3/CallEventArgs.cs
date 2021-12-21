using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
   public struct CallEventArgs
    {
        public string TargetPhoneNumber { get; set; }
        public string SourcePhoneNumber { get; set; }
        public CallState CallState { get; set; }
    }
}
