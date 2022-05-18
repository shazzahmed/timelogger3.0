using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeloggerCore.Data.Entities
{
	public class WorkSession
	{
		public int Id { get; set; }
		public string ScreenShot { get; set; }
		public DateTime ShotTime { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? StopTime { get; set; }
		public int MouseClicked { get; set; }
		public int KeyLogs { get; set; }
		public int TimeLogId { get; set; }
		public bool IsDeleted { get; set; }
		public int ElapsedMinutes { get; set; }
		public string ActiveWindow { get; set; }
		public TimeSpan IdleTime { get; set; }
		public string Description { get; set; }
		[ForeignKey("TimeLogId")]
		public TimeLog TimeLog { get; set; }
		public bool IsCompleteWork { get; set; }
		public DateTime? UnCompleteStartTime { get; set; }
		public TimeSpan? UnCompleteTotalTime { get; set; }
	}
}
