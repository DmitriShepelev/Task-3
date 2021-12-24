using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.Interfaces
{
    public interface ITariff
    {
        public int Id { get; }
        public string Name { get; }
        public decimal PricePerMinute { get; }
    }
}
