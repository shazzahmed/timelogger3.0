using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeloggerCore.Data.Entities
{
    public class ClientAgency : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string AgencyId { get; set; }
        public bool IsAgencyAccepted { get; set; }
        [ForeignKey("ClientId")]
        public virtual ApplicationUser Client { get; set; }
        [ForeignKey("AgencyId")]
        public virtual ApplicationUser Agency { get; set; }
    }
}
