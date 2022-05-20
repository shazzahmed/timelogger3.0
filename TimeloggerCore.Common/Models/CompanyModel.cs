using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Website { get; set; }


        public virtual ICollection<AddressModel> Addresses { get; set; }
        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}
