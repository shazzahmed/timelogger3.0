using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class ProjectsInvitation :  BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string AgencyId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAccepted { get; set; }
        public bool ExistingUser { get; set; }
        
        public InvitationType InvitationType { get; set; }
        
        [ForeignKey("AgencyId")]
        public virtual ApplicationUser Agency { get; set; }
        
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
