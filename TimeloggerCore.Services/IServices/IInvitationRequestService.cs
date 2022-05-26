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
        Task<BaseModel> AddInvitation(InvitationRequestModel dd, string transactionCode);
        Task<BaseModel> GetClientAgency(InvitationRequestModel invitationRequest);
        Task<BaseModel> GetClientAgencies(InvitationRequestModel invitationRequest);
        Task<BaseModel> GetOnlyClientAgencies(string userId);
    }
}
