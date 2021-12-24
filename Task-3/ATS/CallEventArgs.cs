using Task_3.ATS.States;

namespace Task_3.ATS
{
   public struct CallEventArgs
    {
        public  int Id { get; set; }
        public string TargetPhoneNumber { get; set; }
        public string SourcePhoneNumber { get; set; }
        public CallState CallState { get; set; }
    }
}
