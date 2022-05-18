using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class InvitationRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("InvitationSentFrom")]
        public string FromUserId { get; set; }
        [ForeignKey("InvitationSentTo")]
        public string ToUserId { get; set; }
        public bool IsAccepted { get; set; }
        public bool ExistingUser { get; set; }
        public InvitationType InvitationType { get; set; }
        virtual public ApplicationUser InvitationSentFrom { get; set; }
        public ApplicationUser InvitationSentTo { get; set; }

    }
}
