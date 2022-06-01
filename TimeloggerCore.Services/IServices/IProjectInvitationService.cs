using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Services.IService
{
    public interface IProjectInvitationService : IBaseService<ProjectsInvitationModel, ProjectsInvitation, int>
    {
        Task<BaseModel> ProjectAssign(string userId, ClientWorkersModel clientWorkerModel, InvitationType invitationType);
    }
}
