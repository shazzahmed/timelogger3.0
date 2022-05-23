using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class Payment : BaseEntity
    {
        public static string[] PlanName =
            {
            "Mini(Free/mo - 2 Team Member)",
            "Small($15 / mo - 5 Team Member)",
            "Medium($25 / mo - 7 Team Member)",
            "Large($40 / mo - 10 Team Member)",
            "Create Your Own Package"
            };
        public static string[] PaymentMethodType =
            {
            "PayPal",
            "Pay To Bank (Cheque)",
            "PickUp (Cash Up)"
            };

        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Package")]
        public int PackageTypeId { get; set; }
        public PaymentType PaymentTypeId { get; set; }
        public float PaymentAmount { get; set; }
        public float? PaymentDueAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public PaymentDuration PaymentDuration { get; set; }
        public string PaypalTrasactionId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? LastInvoiceGenerated { get; set; }

        public DateTime? BillingDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsEnquiry { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ApplicationUser User { get; set; }
        public Package Package { get; set; }

        public bool ispaidOther { get; set; }

        public float ExtraAmount { get; set; }

        public PaymentStatus ExtraPaymentStatus { get; set; }

        public bool IsCancel { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
