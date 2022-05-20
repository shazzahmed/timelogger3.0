using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class DashboardModel
    {
        public int Projects { get; set; }
        public int TotalHours { get; set; }
        public int Users { get; set; }
        public List<TimeLogModel> ActivityLogs { get; set; }
        public List<ProjectModel> ProjectsCompleted { get; set; }
        public int Type { get; set; }
    }
    public class DashboardBaseModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<TimeLogModel> TimeLogs { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public List<InvitationModel> Invitations { get; set; }
        public List<ClientWorkerModel> ClientWorkerInvitation { get; set; }
        public List<WorkSessionModel> WorkSessionViewModels { get; set; }
    }
    public class DashboardDataModel
    {
        public string UserId { get; set; }
        public int Type { get; set; }
        public bool IsWorkSessionRequired { get; set; }
    }
}
