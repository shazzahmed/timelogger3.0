using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class PaymentModel : BaseClass
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
        public PackageModel Package { get; set; }

        public bool ispaidOther { get; set; }

        public float ExtraAmount { get; set; }

        public PaymentStatus ExtraPaymentStatus { get; set; }

        public bool IsCancel { get; set; }
    }
    public class PaymentsModel
    {
        public List<PaymentModel> Payments { get; set; }
        public List<InvitationModel> Invitations { get; set; }
        public PackageModel Packages { get; set; }
        public int TotalProject { get; set; }
        public List<TimeLogModel> EmployeeWorkingHour { get; set; }
        public string TotalWorkHour { get; set; }
    }
    public class PackageViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int NoOfUsers { get; set; }
    }
}
