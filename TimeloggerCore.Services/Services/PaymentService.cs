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
            var result = await _paymentRepository.GetAllRecurringPayment();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Payment>, List<PaymentModel>>(result)
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
        public async Task<BaseModel> GetActivePayment(int paymentId)
        {
            var result = await _paymentRepository.GetActivePayment(paymentId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Payment, PaymentModel>(result)
            };
        }
        public async Task<BaseModel> GetAllActiveClient()
        {
            var result = await _paymentRepository.GetAllActiveClient();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Payment>, List<PaymentModel>>(result)
            };
        }
        public async Task<BaseModel> GetAllPendingClient()
        {
            var result = await _paymentRepository.GetAllPendingClient();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Payment>, List<PaymentModel>>(result)
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
