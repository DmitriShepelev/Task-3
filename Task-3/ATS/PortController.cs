using System.Collections.Generic;

namespace Task_3.ATS
{
    public  class PortController
    {
        private  readonly Dictionary<string, Port> _dictionary = new();

        public  Port GetPort(string targetNumber)
        {
            _dictionary.TryGetValue(targetNumber, out var result);
            return result;
        }

        public  void AddMatchPair(string phoneNumber, Port port)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber) && port != null)
                _dictionary.Add(phoneNumber, port);
        }

        //public  void ChangePhonePort(string phoneNumber, Port port)
        //{
        //    if (_dictionary.ContainsKey(phoneNumber) && port != null) _dictionary[phoneNumber] = port;
        //}
    }
}