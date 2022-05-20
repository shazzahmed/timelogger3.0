using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class ProjectsInvitation
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [ForeignKey("Agency")]
        public string AgencyId { get; set; }
        public InvitationType InvitationType { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAccepted { get; set; }
        public bool ExistingUser { get; set; }
        public ApplicationUser Agency { get; set; }
        public ProjectModel Project { get; set; }
    }
    public class ProjectInvitationModel
    {
        public List<ProjectModel> Projects { get; set; }
        public List<InvitationModel> Invitations { get; set; }
        public List<ClientWorkerModel> WorkerInvitation { get; set; }
        public ApplicationUser CurrentUser { get; set; }
    }
}
