using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class WorkSessionModel
    {
        public int Id { get; set; }
        public byte[] ScreenShot { get; set; }
        public string ScreenShotUrl { get; set; }
        public DateTime ShotTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public int MouseClicked { get; set; }
        public int KeyLogs { get; set; }
        public int TimeLogId { get; set; }
        public int ElapsedMinutes { get; set; }
        public string ActiveWindow { get; set; }
        public TimeSpan IdleTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public TimeLogModel TimeLog { get; set; }
        public bool? IsCompleteWork { get; set; }
        public DateTime? UnCompleteStartTime { get; set; }
        public TimeSpan? UnCompleteTotalTime { get; set; }
        public string ScreenShot64 { get; set; }
        public string SessionTime { get; set; }
        public List<string> KeyTime { get; set; }
        public TimeSpan workDuration { get; set; }
    }
    public class WorkSessionTimeModel
    {
        public DateTime StartTime { get; set; }
        public DateTime LastStartTime { get; set; }
        public DateTime LastEndTime { get; set; }
        public TimeSpan TotalSessionTime { get; set; }

    }
}
