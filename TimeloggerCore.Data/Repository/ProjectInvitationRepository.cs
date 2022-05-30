using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Repository
{
    public class ProjectInvitationRepository : BaseRepository<ProjectsInvitation, int>, IProjectInvitationRepository
    {
        public ProjectInvitationRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<ProjectsInvitation> ProjectAssign(string ProjectId, string AgencyId, InvitationType invitationType)
        {
            int projectId = Convert.ToUInt16(ProjectId);
            var projectInvitation = await FirstOrDefaultAsync(
                 x => x.AgencyId == AgencyId
                 && x.ProjectId == projectId
                 && x.InvitationType == invitationType
                 && !x.IsDeleted,
                 null,
                 i => i.Agency);
            return projectInvitation;
        }
    }
}
