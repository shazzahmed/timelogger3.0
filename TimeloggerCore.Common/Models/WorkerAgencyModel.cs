using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class WorkerAgencyModel
    {
        public ApplicationUserModel WorkerAgency { get; set; }
        public bool IsProjectExit { get; set; }
        public bool IsAgencyAccepted { get; set; }
        public bool IsWorkerHasAgency { get; set; }
        public bool IsPaymentExpire { get; set; }
        public TimeLogModel TimeLogViewModel { get; set; }
    }
}
