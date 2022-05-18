using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Invitation> Invitations { get; set; }
        public List<TimeLog> TimeLogs { get; set; }
    }
}
