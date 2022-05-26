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
        private readonly IAgencyService _agencyService;

        public InvitationRequestService(
            IMapper mapper, IInvitationRequestRepository invitationRequestRepository, IUnitOfWork unitOfWork, IAgencyService agencyService
            ) : base(mapper, invitationRequestRepository, unitOfWork)
        {
            _invitationRequestRepository = invitationRequestRepository;
            _agencyService = agencyService;
        }
        public Task<BaseModel> AddInvitation(InvitationRequestModel dd, string transactionCode)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseModel> GetClientAgency(InvitationRequestModel invitationRequest)
        {
            var userFound = await _invitationRequestRepository.GetClientAgency(new AgencyAlreadyExitModel() { AgencyEmail = invitationRequest.ToUserId, UserId = invitationRequest.FromUserId });
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<InvitationRequest, InvitationRequestModel>(userFound)
            };
        }
        public async Task<BaseModel> GetClientAgencies(InvitationRequestModel invitationRequest)
        {
            var userFound = await _invitationRequestRepository.GetClientAgencies(new AgencyAlreadyExitModel() { AgencyEmail = invitationRequest.ToUserId, UserId = invitationRequest.FromUserId });
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<InvitationRequest>, List<InvitationRequestModel>>(userFound)
            };
        }
        public async Task<BaseModel> GetOnlyClientAgencies(string userId)
        {
            var invitationRequest = await _invitationRequestRepository.GetOnlyClientAgencies(userId);
            var ClientAgencies = await _agencyService.GetClientAgencies(userId);

            return new BaseModel
            {
                Success = true,
                Data = new ClientAgenciesModel
                {
                    ClientAgencies = mapper.Map<List<ClientAgency>, List<ClientAgencyModel>>((List<ClientAgency>)ClientAgencies.Data),
                    InvitationRequest = mapper.Map<List<InvitationRequest>, List<InvitationRequestModel>>(invitationRequest)
                }
            };
        }
    }
}
