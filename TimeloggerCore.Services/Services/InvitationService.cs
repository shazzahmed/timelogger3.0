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
    public class InvitationService : BaseService<InvitationModel, Invitation, int>, IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationService(
            IMapper mapper, 
            IInvitationRepository invitationRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, invitationRepository, unitOfWork)
        {
            _invitationRepository = invitationRepository;
        }
        public async Task<BaseModel> GetActiveProjects(string userId)
        {
            var result = await _invitationRepository.GetActiveProjects(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Invitation>, List<InvitationModel>>(result)
            };
        }
        public async Task<BaseModel> GetInvitationsList(string userId)
        {
            var result = await _invitationRepository.GetInvitationsList(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Invitation>, List<InvitationModel>>(result)
            };
        }
    }
}
