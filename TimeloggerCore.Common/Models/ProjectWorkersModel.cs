using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class ProjectWorkersModel : BaseClass
    {
        public int Id { get; set; }
        [ForeignKey("Worker")]
        public string WorkerId { get; set; }
        [ForeignKey("ProjectsInvitation")]
        public int? ProjectsInvitationId { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public bool ExistingUser { get; set; }
        public string EmailAddress { get; set; }
        public WorkerType WorkerType { get; set; }
        public bool IsAccepted { get; set; }
        public bool CanEditTimeLog { get; set; }
        public bool CanAddManualTime { get; set; }
        public double? RatePerHour { get; set; }
        public double? MinimumHours { get; set; }
        public double? MaximumHours { get; set; }
        public MemberStatus Status { get; set; }
        public int ProjectHours { get; set; }

        public ProjectModel Project { get; set; }
        public ApplicationUser Worker { get; set; }
        public ProjectModel ProjectsInvitation { get; set; }
    }
}
