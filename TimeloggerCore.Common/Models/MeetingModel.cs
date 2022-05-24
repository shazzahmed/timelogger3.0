using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class MeetingModel
    {
        public int Id { get; set; }
        public string Agenda { get; set; }
        public string Description { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        //public MeetingDuration MeetingDurations { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public ApplicationUserModel User { get; set; }
    }
}
