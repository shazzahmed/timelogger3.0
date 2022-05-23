using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Data.IRepository
{
    public interface IInvitationRequestRepository : IBaseRepository<InvitationRequest, int>
    {
        Task<InvitationRequest> GetClientAgency(AgencyAlreadyExitModel agencyAlreadyExit);
        Task<List<InvitationRequest>> GetOnlyClientAgencies(string userId);
        Task<List<InvitationRequest>> GetClientAgencies(AgencyAlreadyExitModel agencyAlreadyExit);
    }
}
