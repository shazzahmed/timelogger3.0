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

namespace TimeloggerCore.Services
{
    public class PaymentService : BaseService<PaymentModel, Payment, int>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvitationRequestRepository _invitationRequestRepository;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork) : base(mapper, paymentRepository, unitOfWork)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<int> GetUserByEmail(InvitationRequest invitationRequest)
        {
            var isUserExit = await _paymentRepository.GetUserByEmail(invitationRequest.ToUserId);
            invitationRequest.ToUserId = isUserExit.Id;
            invitationRequest.IsActive = false;
            return 0;
            //_invitationRequestRepository.AddAsync(invitationRequest);
            //int isUserCreated = await _invitationRequestRepository.Save();
            //return isUserCreated;
        }

        public async Task<ApplicationUser> UserInfo(string userId)
        {
            var userInfo = await _paymentRepository.UserInfo(userId);
            return userInfo;
        }
    }
}
