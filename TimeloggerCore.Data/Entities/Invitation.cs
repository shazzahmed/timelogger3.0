using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class Invitation
    {
        [Key]
        public int ID { get; set; }
        public virtual Project Project { get; set; }
        public bool ExistingUser { get; set; }
        public string UserID { get; set; }

        public string ClientID { get; set; }

        [ForeignKey("UserID")]
        [InverseProperty("UserInvitations")]
        public ApplicationUser User { get; set; }

        [ForeignKey("ClientID")]
        [InverseProperty("ClientInvitations")]
        public ApplicationUser Client { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        public string EmailAddress { get; set; }
        public bool IsAccepted { get; set; }
        public bool CanEditTimeLog { get; set; }
        public bool CanAddManualTime { get; set; }
        public double RatePerHour { get; set; }
        public double MinimumHours { get; set; }
        public double MaximumHours { get; set; }
        public MemberStatus Status { get; set; }
        public ProjectOwner ProjectOwner { get; set; }
        public int ProjectHours { get; set; }
    }
}
