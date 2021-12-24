using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Interfaces;

namespace Task_3.MobileOperator
{
    public class TariffMax : ITariff
    {
        public int Id => 222;

        public string Name => "Max";

        public decimal PricePerMinute => 100500m;
    }
}
