using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Data.Repository
{
    public class ClientAgencyRepository : BaseRepository<ClientAgency, int>, IClientAgencyRepository
    {
        public ClientAgencyRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<ClientAgency>> GetClientAgencies(string userId)
        {
            var clientAgencies = await DbContext.ClientAgency.Where(x => !x.IsDeleted && x.ClientId == userId).Include(x => x.Agency).ToListAsync();
            return clientAgencies;
        }
        public async Task<List<ClientAgency>> GetAgencyClients(string userId)
        {
            var clientAgencies = await DbContext.ClientAgency.Where(x => !x.IsDeleted && x.AgencyId == userId && x.IsAgencyAccepted).Include(x => x.Client).ToListAsync();
            return clientAgencies;
        }
        public async Task<ClientAgency> GetSingleClientAgencies(int Id)
        {
            var singleClientAgency = await DbContext.ClientAgency.Where(x => x.Id == Id).Include(x => x.Agency).Include(x => x.Client).FirstOrDefaultAsync();
            return singleClientAgency;
        }
        public async Task<ClientAgency> GetClientAgency(AgencyAlreadyExitModel agencyAlreadyExit)
        {
            var clientAgency = await DbContext.ClientAgency.Where(x => x.ClientId == agencyAlreadyExit.UserId && x.Agency.Email == agencyAlreadyExit.AgencyEmail).FirstOrDefaultAsync();
            return clientAgency;
        }
        public async Task<List<Entities.ApplicationUser>> GetAgencyEmployee(string AgencyId)
        {
            var agencyWorker = await DbContext.User.Where(x => x.AgencyId == AgencyId && x.IsWorkerHasAgency && x.IsAgencyApproved).ToListAsync();
            //&& x.IsWorkerHasAgency&&x.IsAgencyApproved
            return agencyWorker;

        }

        public async Task<List<Entities.ApplicationUser>> GetAllWorker()
        {
            var worker = await DbContext.User.Where(x => !x.IsWorkerHasAgency).ToListAsync();
            return worker;
        }
    }
}
