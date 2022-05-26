using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IInvitationRequestService : IBaseService<InvitationRequestModel, InvitationRequest, int>
    {
        Task<bool> AddInvitation(InvitationRequest dd, string transactionCode);
        Task<InvitationRequest> GetClientAgency(InvitationRequest invitationRequest);
        Task<List<InvitationRequest>> GetClientAgencies(InvitationRequest invitationRequest);
        Task<List<InvitationRequest>> GetOnlyClientAgencies(string userId);
    }
}
