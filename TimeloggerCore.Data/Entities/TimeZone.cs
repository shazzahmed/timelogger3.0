using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeloggerCore.Data.Entities
{
    public class TimeZone
    {
        [Key]
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string StandardName { get; set; }

        public List<ApplicationUser> ApplicationUser { get; set; }
    }
}
