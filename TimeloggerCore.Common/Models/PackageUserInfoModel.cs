using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class PackageUserInfoModel
    {
        public ApplicationUser BuyerInfo { get; set; }
        public PackageModel Package { get; set; }
        public PaymentModel Payment { get; set; }
    }
    public class PaypalModel
    {
        public string baseURI { get; set; }
        public string userId { get; set; }
        public string guid { get; set; }
        public float? amount { get; set; }
    }
    public class BuyerInfoModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class PaymentInfoModel
    {
        public int PaymentMethodId { get; set; }
        public int PaymentId { get; set; }
    }
    public class PaymentInquiryModel
    {
        public int Id { get; set; }
        public string PayUnpay { get; set; }
        public string PaypalTrasactionId { get; set; }
        public PaymentType PaymentTypeId { get; set; }
    }
    public class PaymentInquiryViewModel
    {
        public bool Response { get; set; }
        public List<PaymentModel> AllPayment { get; set; }
    }
    public class PlanChangeModel
    {
        public string UserId { get; set; }
        public string PackageId { get; set; }

        public string AllowMember { get; set; }
    }
}
