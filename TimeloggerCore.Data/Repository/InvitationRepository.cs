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
        public async Task<List<Invitation>> GetActiveProjects(string userId)
        {
            var test = DbContext.Invitations.ToList();
            var invitations = await GetAsync(
                 x =>
                 x.ClientID == userId && x.Status == MemberStatus.Active,
                 null,
                 i => i.Project, i => i.User);
            return invitations;
        }
        public async Task<List<Invitation>> GetInvitationsList(string userId)
        {
            var invitationList = await GetAsync(
                 x =>
                 x.ClientID == userId,
                 null,
                 i => i.Project, i => i.User);
            return invitationList;
        }
    }
}
