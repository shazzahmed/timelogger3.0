using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class WorkDiarySessionModel
    {
        public string LogTime { get; set; }
        public string AutoTrackedTime { get; set; }
        public string ManualTrackedTime { get; set; }
        public string LogDate { get; set; }
        public TimeSpan? MeetingTime { get; set; }
        public string[] sessionDurationHourMinSec { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
        public List<List<List<WorkSessionModel>>> WorkSessions { get; set; }
    }
}
