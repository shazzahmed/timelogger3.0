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
            var clientAgencies = await GetAsync(
                 x =>
                 !x.IsDeleted && x.ClientId == userId,
                 null,
                 i => i.Agency);
            return clientAgencies;
        }
        public async Task<List<ClientAgency>> GetAgencyClients(string userId)
        {
            var clientAgencies = await GetAsync(
                 x =>
                 !x.IsDeleted && x.AgencyId == userId && x.IsAgencyAccepted,
                 null,
                 i => i.Client);
            return clientAgencies;
        }
        public async Task<ClientAgency> GetSingleClientAgencies(int Id)
        {
            var singleClientAgency = await FirstOrDefaultAsync(
                 x =>
                 x.Id == Id,
                 null,
                 i => i.Agency, i => i.Client);
            return singleClientAgency;
        }
        public async Task<ClientAgency> GetClientAgency(ClientAgencyModel clientAgencyModel)
        {
            var clientAgency = await FirstOrDefaultAsync(
                 x =>
                 x.ClientId == clientAgencyModel.ClientId && x.AgencyId == clientAgencyModel.AgencyId,
                 null,
                 i => i.Agency, i => i.Client);
            return clientAgency;
        }
    }
}
