using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.IRepository
{
    public interface IProjectInvitationRepository : IBaseRepository<ProjectsInvitation, int>
    {
        Task<ProjectsInvitation> ProjectAssign(string ProjectId, string AgencyId, InvitationType invitationType);
    }
}
