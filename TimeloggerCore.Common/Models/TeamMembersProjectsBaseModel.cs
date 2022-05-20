using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class TeamMembersProjectsBaseModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<ProjectModel> Projects { get; set; }

        public List<ApplicationUser> Agency { get; set; }
    }
    public class TeamMembersProjectsViewModel : TeamMembersProjectsBaseModel
    {
        [Display(Name = "Team Member")]
        public string UserID { get; set; }
        [Display(Name = "Project")]
        public int ProjectID { get; set; }

        public string FullName { get; }

        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }
    }
}
