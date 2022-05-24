using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class GroupedInvitationModel
    {
        public ApplicationUserModel User { get; set; }
        public List<TimeLogModel> TimeLogs { get; set; }
        public string TimeLog { get; set; }
    }
}
