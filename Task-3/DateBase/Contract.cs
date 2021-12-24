using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.DateBase
{
   public class Contract
   {
       public int Id => this.GetHashCode();
       public DateTime SignDate { get; set; }
       public int PhoneId { get; set; }
       public int TariffId { get; set; }
       public int ClientId { get; set; }
       //public decimal Balance { get; set; }
       public Contract(DateTime signDate, int phoneId, int tariffId, int clientId)
       {
           SignDate = signDate;
           PhoneId = phoneId;
           TariffId = tariffId;
           ClientId = clientId;
       }
   }
}
