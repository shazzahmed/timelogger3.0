using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class TimeLog
    {
        [Key]
        public int Id { get; set; }

        public DateTime LogDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Project")]

        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Description { get; set; }
        public TrackType TrackType { get; set; }
        public List<WorkSession> WorkSessions { get; set; }
    }
}
