using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

namespace Task_3
{
    public static class PortController
    {
        private static readonly Dictionary<string, Port> Dictionary = new();
        //создать контроллер, инжектировать зависимости
        // public PortController() => _dictionary = new Dictionary<string, Port>();
        //public static PortController Instance => this;

        public static Port GetPort(string targetNumber)
        {
            Dictionary.TryGetValue(targetNumber, out var result);
            return result;
        }

        public static void AddMatchPair(string phoneNumber, Port port)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber) && port != null)
                Dictionary.Add(phoneNumber, port);
        }

        public static void ChangePhonePort(string phoneNumber, Port port)
        {
            if (Dictionary.ContainsKey(phoneNumber) && port != null) Dictionary[phoneNumber] = port;
        }
    }
}