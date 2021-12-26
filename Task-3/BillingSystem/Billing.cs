using System.Linq;
using Task_3.ATS;
using Task_3.DateBase;

namespace Task_3.BillingSystem
{
    internal class Billing
    {
        private readonly DataBase _db;
        private readonly Station _station;

        internal Billing(DataBase dataBase, Station station)
        {
            _db = dataBase;
            _station = station;
            _station.CallRecordCreate += OnCallRecordCreated;
        }
        internal void OnCallRecordCreated(object sender, Call call)
        {
            var phone = _db.Phones.Single(ph => ph.PhoneNumber == call.SourceNumber);
            var contract = _db.Contracts.Single(c => c.PhoneId == phone.Id);
            call.ContractId = contract.Id;
            call.CallCost = this.CalculateCost(call, contract);

            _db.Calls.Add(call);
        }

        private decimal CalculateCost(Call call, Contract contract)
        {
            if (call.StartCall == call.EndCall) return 0;
            
            var tariff = _db.Tariffs.Single(t => t.Id == contract.TariffId);

            return (call.EndCall - call.StartCall).Seconds * tariff.PricePerSecond;
        }
    }
}

