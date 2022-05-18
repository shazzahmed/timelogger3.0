using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class ProjectInvitations
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [ForeignKey("InvitationRequest")]
        public int InvitationRequestId { get; set; }
        virtual public InvitationRequest InvitationRequest { get; set; }
        public MemberStatus Status { get; set; }
        public Project Project { get; set; }
        public bool IsAccepted { get; set; }

    }
}
