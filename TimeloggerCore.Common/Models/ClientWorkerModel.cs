using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class ClientWorkerModel
    {
        public int Id { get; set; }

        [ForeignKey("Worker")]
        public string WorkerId { get; set; }

        [ForeignKey("ProjectsInvitation")]
        public int? ProjectsInvitationId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public bool ExistingUser { get; set; }
        public string EmailAddress { get; set; }
        public WorkerType WorkerType { get; set; }
        public bool IsAccepted { get; set; }
        public bool CanEditTimeLog { get; set; }
        public bool CanAddManualTime { get; set; }
        public double? RatePerHour { get; set; }
        public double? MinimumHours { get; set; }
        public double? MaximumHours { get; set; }
        public MemberStatus Status { get; set; }
        public int ProjectHours { get; set; }

        public ProjectModel Project { get; set; }
        public ApplicationUser Worker { get; set; }
        public ProjectsInvitation ProjectsInvitation { get; set; }
    }
    public class ClientWorkersModel
    {
        public string[] WorkerIds { get; set; }
        public string ProjectId { get; set; }
        public string AgencyId { get; set; }
        public bool IsWorkerOrAgency { get; set; }
        public WorkerType WorkerType { get; set; }
        public string UserId { get; set; }
        public InvitationType InvitationMemberType { get; set; }
    }
    public class ClientWorkerResponse
    {
        public string Message { get; set; }
        public ClientWorkerStatus ClientWorkerStatus { get; set; }
        public bool Status { get; set; }
        public string View { get; set; }
    }
    public class AddWorkerClientReponse
    {
        public string Message { get; set; }
        public AddWorkerClientStatus WorkerResponse { get; set; }
        public ProjectModel Project { get; set; }
        public ApplicationUser User { get; set; }
        public ClientWorkerModel ClientWorker { get; set; }
        public List<ClientWorkerModel> ClientWorkerList { get; set; }
    }
}
