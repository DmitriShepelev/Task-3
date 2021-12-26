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

        private readonly Station _station;
        private readonly DataBase _dataBase;
        private readonly PortController _portController;
        private readonly Billing _billing;
        public MobileOperator()
        {
            _portController = new PortController();
            _station = new Station(_portController);
            _dataBase = new DataBase();
            _billing = new Billing(_dataBase, _station);
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
            if (!_dataBase.Tariffs.Contains(tariff))
                _dataBase.Tariffs.Add(tariff);
        }

        public IEnumerable<string> GetDetailedReport(Phone phone, int numberOfDays)
        {
            var phoneCalls = _dataBase.Calls.Where(c =>
                c.SourceNumber == phone.PhoneNumber &&
                DateTime.Now.AddDays(-numberOfDays) <= c.StartCall ||
                c.TargetNumber == phone.PhoneNumber &&
                DateTime.Now.AddDays(-numberOfDays) <= c.StartCall
                );

           return this.GetResult(phone, phoneCalls);
        }

        public IEnumerable<string> GetReportByDate(Phone phone, DateTime dateTime)
        {
            var phoneCalls = _dataBase.Calls.Where(c =>
                c.SourceNumber == phone.PhoneNumber &&
                c.StartCall.Date == dateTime.Date ||
                c.TargetNumber == phone.PhoneNumber &&
                c.StartCall.Date == dateTime.Date
            );

            return this.GetResult(phone, phoneCalls);
        }

        public IEnumerable<string> GetReportByCost(Phone phone, decimal startRange, decimal endRange)
        {
            var phoneCalls = _dataBase.Calls.Where(c =>
                c.SourceNumber == phone.PhoneNumber );
            var phoneCallsByDuration = phoneCalls.Where(c =>
                c.CallCost >= startRange && c.CallCost <= endRange);
            return this.GetResult(phone, phoneCalls);
        }

        public IEnumerable<string> GetReportFilteredBySpecificSubscriber(Phone phone1, Phone phone2)
        {
            var phoneCalls = _dataBase.Calls.Where(c =>
                c.SourceNumber == phone1.PhoneNumber && c.TargetNumber == phone2.PhoneNumber ||
                c.TargetNumber == phone1.PhoneNumber && c.SourceNumber == phone2.PhoneNumber);
            return this.GetResult(phone1, phoneCalls);
        }

        private IEnumerable<string> GetResult(Phone phone, IEnumerable<Call> calls)
        {
            foreach (var call in calls)
            {
                var x = phone.PhoneNumber == call.TargetNumber;
                yield return
                    $"Call duration: {(call.EndCall - call.StartCall).Seconds} seconds \t " +
                    $"Call cost: {(x ? 0.00m : call.CallCost)}BYN \t " +
                    $"\"{call.SourceNumber}\" calls \"{call.TargetNumber}\"";
            }
        }
    }
}
