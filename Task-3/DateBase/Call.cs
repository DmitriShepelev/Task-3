using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.DateBase
{
    public class Call
    {
        public int Id => this.GetHashCode();
        public DateTime StartCall { get; set; }
        public DateTime EndCall { get; set; }
        public string SourceNumber { get; set; }
        public string TargetNumber { get; set; }
        public int ContractId { get; set; }
        public decimal CallCost { get; set; }
    }
}
