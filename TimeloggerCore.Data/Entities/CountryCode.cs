using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeloggerCore.Data.Entities
{
    public class CountryCode : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string CountryShortCode { get; set; }
        public int CountryValue { get; set; }
    }
}
