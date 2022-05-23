using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Repository
{
    public class InvitationRepository : BaseRepository<Invitation, int>, IInvitationRepository
    {
        public InvitationRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public List<Invitation> GetActiveProjects(string userId)
        {
            var test = DbContext.Invitations.ToList();
            var invitations = DbContext.Invitations.Include(x => x.Project).Include(x => x.User).Where(i => i.ClientID == userId && i.Status == MemberStatus.Active).ToList();
            return invitations;
        }
        public List<ClientWorker> GetClientActiveProjects(string userId)
        {
            var invitations = DbContext.ClientWorker.Include(x => x.Project).Include(x => x.Worker).Where(i => i.ProjectsInvitation.AgencyId == userId && i.Status == MemberStatus.Active).Include(x => x.ProjectsInvitation)
                .Include(x => x.Worker).ToList();

            //var invitations = context.Invitations.Include(x=>x.Project).Include(x => x.User).Where(i => i.ClientID == userId && i.Status == MemberStatus.Active).ToList();
            return invitations;
        }

        public async Task<List<Invitation>> GetInvitationsList(string userId)
        {
            var invitationList = await DbContext.Invitations.Where(x => x.ClientID == userId).Include(x => x.User).Include(x => x.Project).ToListAsync();
            return invitationList;
        }
    }
}
