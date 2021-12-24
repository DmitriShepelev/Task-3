using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.ATS;
using Task_3.Interfaces;

namespace Task_3.DateBase
{
    internal class DataBase
    {
        internal ICollection<Call> Calls = new List<Call>();
        internal ICollection<Client> Clients = new List<Client>();
        internal ICollection<Contract> Contracts = new List<Contract>();
        internal ICollection<ITariff> Tariffs = new List<ITariff>();
        internal ICollection<Phone> Phones = new List<Phone>();
    }
}
