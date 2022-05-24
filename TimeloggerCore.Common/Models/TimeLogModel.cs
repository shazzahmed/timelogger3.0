using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class TimeLogModel
	{
		public int Id { get; set; }

		public DateTime LogDate { get; set; }
		//[DisplayFormat(DataFormatString = "{0:hh:mm:ss tt}")]

		public DateTime StartTime { get; set; }
		//[DisplayFormat(DataFormatString = "{0:hh:mm:ss tt}")]

		public string Title { get; set; }
		public DateTime? StopTime { get; set; }

		public bool IsActive { get; set; }

		public int ProjectId { get; set; }
		public ProjectModel Project { get; set; }
		public ApplicationUserModel User { get; set; }
		public string UserId { get; set; }
		public string Description { get; set; }

		public string LogTime { get; set; }
		public string ActiveTime { get; set; }
		public string TimeStart { get; set; }
		public string CurrentTime { get; set; }
		public TrackType TrackType { get; set; }
		public bool IsDeleted { get; set; }
		public List<WorkSessionModel> WorkSessions { get; set; }
		//public Project Project { get; set; }

		public TimeLogModel()
		{
			WorkSessions = new List<WorkSessionModel>();
		}
	}

	public class ReportModel
	{
		public List<TimeLogModel> Timelogs { get; set; }
		public List<WorkSessionModel> WorkSessions { get; set; }
	}
}
