using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Project title is required.")]
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        //public CompanyViewModel Company { get; set; }
        //[Required]
        //[Display(Name = "Company")]
        //public int CompanyId { get; set; }

        //public List<CompanyViewModel> Companies { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsActive { get; set; } = false;
        public double Percentage { get; set; }
        public ProjectInvitationModel ProjectInvitationModel { get; set; }
        public List<ApplicationUser> ApplicationUserList { get; set; }

        //   public List<TimeLogViewModel> TimeLogs { get; set; }
        public List<InvitationModel> Invitations { get; set; }
    }
    public class ProjectWithInvitationModel
    {
        public List<ProjectModel> OwnProject { get; set; }
        public List<ClientWorkerModel> ProjectInviation { get; set; }
        public List<ProjectModel> UserProjectInvitation { get; set; }
    }
    public class UserProjectViewModel
    {
        public string UserId { get; set; }
        public WorkerType WorkerInvitationType { get; set; }
    }
}
