using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;

namespace TimeloggerCore.Data.IRepository
{
    public interface IClientAgencyRepository : IBaseRepository<ClientAgency, int>
    {
        Task<List<ClientAgency>> GetClientAgencies(string userId);
        Task<List<ClientAgency>> GetAgencyClients(string userId);
        Task<ClientAgency> GetSingleClientAgencies(int Id);
        Task<ClientAgency> GetClientAgency(AgencyAlreadyExitModel agencyAlreadyExit);
        Task<List<ApplicationUser>> GetAgencyEmployee(string AgencyId);
        Task<List<ApplicationUser>> GetAllWorker();
    }
}
