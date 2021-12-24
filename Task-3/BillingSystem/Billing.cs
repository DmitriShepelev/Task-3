using Task_3.ATS;
using Task_3.DateBase;

namespace Task_3.BillingSystem
{
    internal class Billing
    {
        private DataBase _db;

        internal Billing(DataBase dataBase)
        {
            _db = dataBase;
        }
        internal void OnCallRecordCreated(object sender, CallEventArgs args)
        {

        }
    }
}

