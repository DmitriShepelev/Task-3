using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Interfaces;

namespace Task_3.MobileOperator
{
    public class TariffLight : ITariff
    {
        public int Id => 111;
        public string Name => "Light";
        public decimal PricePerSecond => 0.15m;

    }
}
