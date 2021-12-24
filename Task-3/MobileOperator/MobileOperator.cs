using System;
using System.Collections.Generic;
using System.Linq;
using Task_3.ATS;
using Task_3.BillingSystem;
using Task_3.DateBase;
using Task_3.Interfaces;

namespace Task_3.MobileOperator
{
    public class MobileOperator
    {

        private Station _station;
        private readonly DataBase _dataBase;
        private Billing _billing;
        private PortController _portController;
        public MobileOperator()
        {
            _portController = new PortController();
            _station = new Station(_portController);
            _dataBase = new DataBase();
            _billing = new BillingSystem.Billing(_dataBase);
        }

        public Phone SignContract(Client client, ITariff tariff, string phoneNumber)
        {
            var newPhone = new Phone(phoneNumber, _station);

            _dataBase.Phones.Add(newPhone);

            if (!_dataBase.Clients.Contains(client))
                _dataBase.Clients.Add(client);

            if (!_dataBase.Tariffs.Contains(tariff)) 
                _dataBase.Tariffs.Add(tariff);

            _dataBase.Contracts.Add(new Contract(DateTime.Now, newPhone.Id, tariff.Id, client.Id));

            _portController.AddMatchPair(newPhone.PhoneNumber, newPhone.ConnectPort());

            return newPhone;
        }

        public void ChangeTariff(Client client, ITariff tariff)
        {
            var contract = _dataBase.Contracts.SingleOrDefault(contract => contract.ClientId == client.Id);
            if (contract != null) contract.TariffId = tariff.Id;
        }

        //public void Print(Client client)
        //{
        //    var contract = _dataBase.Contracts.SingleOrDefault(contract => contract.ClientId == client.Id);
        //    if (contract != null) Console.WriteLine(contract.TariffId);
        //}
    }
}
