using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IInvitationService : IBaseService<InvitationModel, Invitation, int>
    {
        Task<BaseModel> GetActiveProjects(string userId);
        Task<BaseModel> GetInvitationsList(string userId);
    }
}
