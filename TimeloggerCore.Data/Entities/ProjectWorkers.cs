using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class ProjectWorkers : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("Worker")]
        public string WorkerId { get; set; }
        public ApplicationUser Worker { get; set; }
        [ForeignKey("ProjectInvitations")]
        public int ProjectInvitationsId { get; set; }
        virtual public ProjectInvitations ProjectInvitations { get; set; }
        public bool IsAccepted { get; set; }
    }
}
