using System.Collections.Generic;

namespace Task_3
{
    public class PortController
    {
        private ICollection<Port> _items;
        //создать контроллер, инжектировать зависимости
        public IEnumerable<Port> Items { get { return _items; } }
        public PortController(ICollection<Port> items)
        {
            _items = items;
        }

        public Port GetPort(string targetNumber)
        {
            if (targetNumber == "222-22-22") return new Port();
            return null;
        }
    }
}