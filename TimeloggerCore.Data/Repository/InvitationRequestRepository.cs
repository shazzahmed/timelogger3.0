using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Repository
{
    public class InvitationRequestRepository : BaseRepository<InvitationRequest, int>, IInvitationRequestRepository
    {
        public InvitationRequestRepository(ISqlServerDbContext context) : base(context)
        {
        }

        public async Task<InvitationRequest> GetClientAgency(AgencyAlreadyExitModel agencyAlreadyExit)
        {

            var clientAgency = await DbContext.InvitationRequest.Where(x => x.FromUserId == agencyAlreadyExit.UserId && x.InvitationSentTo.Email == agencyAlreadyExit.AgencyEmail).FirstOrDefaultAsync();
            return clientAgency;
        }
        public async Task<List<InvitationRequest>> GetClientAgencies(AgencyAlreadyExitModel agencyAlreadyExit)
        {

            var clientAgency = await DbContext.InvitationRequest.Where(x => x.FromUserId == agencyAlreadyExit.UserId && x.InvitationType == InvitationType.ClientToAgency).ToListAsync();
            return clientAgency;
        }
        public async Task<List<InvitationRequest>> GetOnlyClientAgencies(string userId)
        {
            var clientAgency = await DbContext.InvitationRequest
                .Where(x => (x.FromUserId == userId || x.ToUserId == userId) && (x.InvitationType == InvitationType.ClientToAgency || x.InvitationType == InvitationType.AgencyToClient))
                .Include(x => x.InvitationSentFrom)
                .Include(x => x.InvitationSentTo)
                .Include(x => x.InvitationSentTo.UserRoles)
                .Include(x => x.InvitationSentFrom.UserRoles)
                .ToListAsync();
            return clientAgency;
        }
    }
}
