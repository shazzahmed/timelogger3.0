using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services
{
    public class InvitationService : BaseService<InvitationModel, Invitation, int>, IInvitationService
    {
        private readonly IInvitationRepository invitationRepository;

        public InvitationService(IMapper mapper, IInvitationRepository invitationRepository, IUnitOfWork unitOfWork) : base(mapper, invitationRepository, unitOfWork)
        {
            this.invitationRepository = invitationRepository;
        }
    }
}
