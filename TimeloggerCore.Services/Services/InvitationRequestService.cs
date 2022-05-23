using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services
{
    public class InvitationRequestService : BaseService<InvitationRequestModel, InvitationRequest, int>, IInvitationRequestService
    {
        private readonly IInvitationRequestRepository _invitationRequestRepository;

        public InvitationRequestService(IMapper mapper, IInvitationRequestRepository invitationRequestRepository, IUnitOfWork unitOfWork) : base(mapper, invitationRequestRepository, unitOfWork)
        {
            _invitationRequestRepository = invitationRequestRepository;
        }
        public Task<bool> AddInvitation(InvitationRequest dd, string transactionCode)
        {
            throw new NotImplementedException();
        }

        public async Task<InvitationRequest> GetClientAgency(InvitationRequest invitationRequest)
        {
            var userFound = await _invitationRequestRepository.GetClientAgency(new AgencyAlreadyExitModel() { AgencyEmail = invitationRequest.ToUserId, UserId = invitationRequest.FromUserId });
            return userFound;
        }
        public async Task<List<InvitationRequest>> GetClientAgencies(InvitationRequest invitationRequest)
        {
            var userFound = await _invitationRequestRepository.GetClientAgencies(new AgencyAlreadyExitModel() { AgencyEmail = invitationRequest.ToUserId, UserId = invitationRequest.FromUserId });
            return userFound;
        }
        public async Task<List<InvitationRequest>> GetOnlyClientAgencies(string userId)
        {
            try
            {
                var userFound = await _invitationRequestRepository.GetOnlyClientAgencies(userId);
                //userFound.ForEach(x =>
                //{
                //    x.Role = _userRepository.GetRolesForUserById(userId);
                //}); 
                return userFound;
            }
            catch (Exception ex)
            {
                return new List<InvitationRequest>();
            }
            //return new List<InvitationRequest>();
        }
    }
}
