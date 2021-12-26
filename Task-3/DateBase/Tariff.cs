using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Interfaces;

namespace Task_3.DateBase
{
    public class Tariff : ITariff
    {
        public int Id { get; }
        public string Name { get; }
        public decimal PricePerSecond { get; }
    }
}
