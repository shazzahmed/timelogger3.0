using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeloggerCore.Common.Utility;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class ApplicationUser : IdentityUser
   {
        [MaxLength(60)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Column(TypeName = "DateTime2")]
        public DateTime BirthDate { get; set; }
        public Enums.Gender Gender { get; set; }
        [MaxLength(500)]
        public string Picture { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(60)]
        public string Language { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedAtUtc { get; set; }
       
        [Column(TypeName = "datetime2")]
        public DateTime DisabledDate { get; set; }
        [Required]
        public string TimeZoneId { get; set; }

        public bool IsWorkerHasAgency { get; set; }
        public string AgencyId { get; set; }
        public bool IsAgencyApproved { get; set; }
        public int? CompanyId { get; set; }
        public int? StatusId { get; set; }
        public TwoFactorTypes TwoFactorTypeId { get; set; }



        [ForeignKey("TwoFactorTypeId")]
        public virtual TwoFactorType TwoFactorType { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<TimeLog> TimeLogs { get; set; }
        
        public virtual ICollection<Invitation> ClientInvitations { get; set; }
        public virtual ICollection<Invitation> UserInvitations { get; set; }

        [ForeignKey("TimeZoneId")]
        public virtual TimeZone TimeZone { get; set; }
    }
}
