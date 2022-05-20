using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class WorkDiaryProjectsModel
    {
        public List<ProjectModel> Projects { get; set; }
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}
