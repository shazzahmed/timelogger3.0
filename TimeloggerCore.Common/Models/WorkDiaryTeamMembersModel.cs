using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class WorkDiaryTeamMembersModel
    {
        public List<ApplicationUser> Users { get; set; }
        [Display(Name = "Team Member")]
        public string UserId { get; set; }
        public List<ProjectModel> Projects { get; set; }
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        public string ProjectIds { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        public string MemberId { get; set; }
    }
    public class ClientAgencyWorkerModel
    {
        public string AgencyId { get; set; }
        public string ClientId { get; set; }
        public string MemberId { get; set; }
    }

    public class ClientWorkerInvitationVm
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }

    }
}
