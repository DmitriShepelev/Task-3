using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.DateBase
{
    public class Client
    {
        public int Id => this.GetHashCode() ^ 17;
        public string FirstName { get; }
        public string LastName { get; }

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
