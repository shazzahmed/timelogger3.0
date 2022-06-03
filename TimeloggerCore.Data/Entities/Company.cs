using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeloggerCore.Data.Entities
{
    public class Company : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
    }
}
