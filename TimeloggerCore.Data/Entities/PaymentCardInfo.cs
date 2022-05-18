using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class PaymentCardInfo
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        public CardType CardType { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
