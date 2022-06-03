using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;
using TimeloggerCore.Core.ISecurity;
using static TimeloggerCore.Common.Utility.Enums;
using System.Linq;

namespace TimeloggerCore.Services
{
    public class PaymentService : BaseService<PaymentModel, Payment, int>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(
            IMapper mapper, 
            IPaymentRepository paymentRepository, 
            ISecurityService securityService, 
            IUnitOfWork unitOfWork
            ) : base(mapper, paymentRepository, unitOfWork)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<BaseModel> GetUserInvoice(string userId)
        {
            var result = await _paymentRepository.GetUserInvoice(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Payment>, List<PaymentModel>>(result)
            };
        }
        public async Task<BaseModel> GetUserSingleInvoice(int InvoiceId)
        {
            var result = await _paymentRepository.GetUserSingleInvoice(InvoiceId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Payment, PaymentModel>(result)
            };
        }
        public async Task<BaseModel> GetAllPayment()
        {
            var result = await _paymentRepository.GetAllPayment();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Payment>, List<PaymentModel>>(result)
            };
        }
        public async Task<BaseModel> GetAllRecurringPayment()
        {
            var paymentList = await _paymentRepository.GetAllRecurringPayment();
            //var payment = await _paymentRepository.GetAllPayment();
            foreach (var x in mapper.Map<List<Payment>, List<PaymentModel>>(paymentList))
            {
                if (!x.IsPaid && x.PaypalTrasactionId != null)
                {
                    //var subscription = PayPalSubscriptionsService.InvoiceGenerate(x.PaypalTrasactionId);

                    //var paymentDate = subscription.agreement_details.last_payment_date != null ? subscription.agreement_details.last_payment_date.Substring(0, 10) : subscription.agreement_details.next_billing_date.Substring(0, 10);
                    //newdte.ToString("yyyy-MM-dd");
                    //var aa = paymentDate.Substring(0,10);
                    //string result = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");
                    // here condition is Paypal Accept the payment and now we will update the payment in our DB. 
                    // TodayDate -1 = PayPal_Payment_Date   = SystemDate -1  

                    //if (String.Equals(paymentDate, result))
                    //{
                        x.PaymentStatus = PaymentStatus.Approved;
                        x.IsEnquiry = false;
                        x.IsPaid = true;
                        x.PaymentTypeId = PaymentType.PayPal;
                        x.PaymentDate = DateTime.Now;
                        x.IsRecurring = false;
                    // look why we make it false ?
                    //x.PaypalTrasactionId = x.PaypalTrasactionId;'
                    //x.PaypalTrasactionId = subscription.payer.payer_info.payer_id;
                    await Update(x);
                    //}
                    //paymentInquiryVeiwModel.PayUnpay = "Paid";
                    //paymentInquiryVeiwModel.PaypalTrasactionId = x.PaypalTrasactionId;
                    //paymentInquiryVeiwModel.Id = x.Id;
                    //paymentInquiryVeiwModel.PaymentTypeId = PaymentType.PayPal;
                    //await EnquiryRequest(paymentInquiryVeiwModel);
                }
            }
            return new BaseModel
            {
                Success = true,
                //Data = mapper.Map<List<Payment>, List<PaymentModel>>(result)
            };
        }
        public async Task<BaseModel> CurrentActivePayment(string UserId)
        {
            var result = await _paymentRepository.CurrentActivePayment(UserId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Payment, PaymentModel>(result)
            };
        }
        public async Task<BaseModel> GetActivePayment(PaymentInfoModel paymentInfoModel)
        {
            var result = await _paymentRepository.GetActivePayment(paymentInfoModel.PaymentId);
            var paymentModel = mapper.Map<Payment, PaymentModel>(result);
            paymentModel.PaymentStatus = PaymentStatus.Enquiry;
            paymentModel.IsEnquiry = true;
            paymentModel.IsRecurring = false;
            paymentModel.PaymentTypeId = (PaymentType)paymentInfoModel.PaymentMethodId;
            await Update(paymentModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Payment, PaymentModel>(result)
            };
        }
        public async Task<BaseModel> GetActivePayment(PaymentInquiryModel paymentInquiryModel)
        {
            PaymentInquiryViewModel paymentInquiryViewModel = new PaymentInquiryViewModel();
            var result = await _paymentRepository.GetActivePayment(paymentInquiryModel.Id);
            bool IsUpdated = false;

            if (result != null)
            {
                var paymentModel = mapper.Map<Payment, PaymentModel>(result);
                if (paymentInquiryModel.PayUnpay == "Paid")
                {
                    result.PaymentStatus = PaymentStatus.Approved;
                    result.IsEnquiry = false;
                    result.IsPaid = true;
                    result.ispaidOther = true;
                    result.ExtraPaymentStatus = PaymentStatus.Approved;
                    result.PaymentTypeId = paymentInquiryModel.PaymentTypeId;
                    result.PaymentDate = DateTime.Now;
                    result.LastInvoiceGenerated = DateTime.Now;
                    if (result.PaypalTrasactionId != null || paymentInquiryModel.PaypalTrasactionId != null)
                    {
                        result.PaypalTrasactionId = paymentInquiryModel.PaypalTrasactionId;
                        result.IsRecurring = true;
                    }
                }
                else if (paymentInquiryModel.PayUnpay == "Unpaid")
                {
                    result.PaymentStatus = PaymentStatus.Rejected;
                    result.IsEnquiry = true;
                }

                await Update(paymentModel);
                IsUpdated = true;
            }
            if (IsUpdated)
                paymentInquiryViewModel.AllPayment = mapper.Map<List<Payment>, List<PaymentModel>>(await _paymentRepository.GetAllPayment());

            paymentInquiryViewModel.Response = IsUpdated;

            return new BaseModel
            {
                Success = true,
                Data = paymentInquiryViewModel
            };
        }
        public async Task<BaseModel> GetAllActiveClient()
        {
            DateTime currentDate = DateTime.Now;
            bool isPayment = false;
            var paymentList = (await _paymentRepository.GetAllActiveClient()).OrderByDescending(x => x.Id);
            var paymentModel = new List<PaymentModel>();
            foreach (var payment in mapper.Map<List<Payment>, List<PaymentModel>>((List<Payment>)paymentList))
            {
                // int days = currentDate.Day- payment.CreatedOn.Day ;
                DateTime today = DateTime.Now;
                DateTime previousDate = payment.CreatedOn.AddDays(1);
                int days = DateTime.Compare(previousDate.Date, today.Date);

                if (days == 0)
                {
                    isPayment = true;
                    Payment invoice = new Payment();
                    payment.CreatedOn = DateTime.Now;
                    payment.IsActive = false;
                    payment.IsEnquiry = invoice.IsPaid = false;
                    payment.IsRecurring = payment.PaypalTrasactionId != null ? true : false;
                    payment.PackageTypeId = payment.PackageTypeId;
                    payment.PaymentAmount = payment.PaymentAmount;
                    payment.PaymentDueDate = DateTime.Now.AddDays(7);
                    payment.PaymentDuration = PaymentDuration.Monthly;
                    payment.PaymentStatus = PaymentStatus.Pending;
                    payment.PaypalTrasactionId = payment.PaypalTrasactionId != null ? payment.PaypalTrasactionId : null;
                    payment.UserId = payment.UserId;
                    payment.LastInvoiceGenerated = payment.CreatedOn;
                    //invoice.PaymentTypeId = payment.PaymentTypeId;
                    paymentModel.Add(payment);
                }
            }
            if (isPayment)
                paymentModel = await AddRange(paymentModel);
            await GetAllRecurringPayment();
            return new BaseModel
            {
                Success = true,
                Data = paymentModel
            };
        }
        public async Task<BaseModel> GetAllPendingClient()
        {
            var result = mapper.Map<List<Payment>, List<PaymentModel>>(await _paymentRepository.GetAllPendingClient());
            foreach (var payment in result)
            {
                DateTime today = DateTime.Now;
                DateTime DueDateOneDay = ((System.DateTime)payment.PaymentDueDate).AddDays(-1);
                DateTime DueDateThreeDays = ((System.DateTime)payment.PaymentDueDate).AddDays(3);
                DateTime DueDateSixDays = ((System.DateTime)payment.PaymentDueDate).AddDays(6);
                int day1 = DateTime.Compare(DueDateOneDay.Date, today.Date);
                int day3 = DateTime.Compare(DueDateThreeDays.Date, today.Date);
                int day6 = DateTime.Compare(DueDateSixDays.Date, today.Date);
                if (day1 == 0 || day3 == 0 || day6 == 0) { }
                    //await ReminderEmailPaymentSend(payment);
            }
            return new BaseModel
            {
                Success = true,
                Data = result
            };
        }
        public async Task<BaseModel> CurrentNonActivePayment(string UserId)
        {
            var result = await _paymentRepository.CurrentNonActivePayment(UserId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Payment, PaymentModel>(result)
            };
        }
    }
}
