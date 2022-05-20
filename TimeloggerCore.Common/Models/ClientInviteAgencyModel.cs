using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class ClientInviteAgencyModel
    {
        public ProjectModel Project { get; set; }
        public List<ClientWorkerModel> IndividualWorker { get; set; }
        public List<ProjectWorkersModel> projectWorkers { get; set; }
        public List<ClientWorkerModel> ClientWorker { get; set; }
        public List<ClientAgencyModel> ClientAgencies { get; set; }
        public List<InvitationRequestModel> InvitationRequest { get; set; }
    }
    public class ClientInviteModel
    {
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        //public bool IsAgencyWorker { get; set; }
        public InvitationType InvitationType { get; set; }
    }
    public class DeleteClientWorker
    {
        public int? Id { get; set; }
        public string ProjectId { get; set; }
        public string AgencyId { get; set; }
        public string WorkerId { get; set; }
        // public bool IsAgencyWorker { get; set; }
        public WorkerType WorkerType { get; set; }
    }
}
