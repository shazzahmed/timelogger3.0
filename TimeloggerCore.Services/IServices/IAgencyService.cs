using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;

namespace TimeloggerCore.Services.IService
{
    public interface IAgencyService : IBaseService<ClientAgencyModel, ClientAgency, int>
    {
        Task<List<ClientAgency>> GetClientAgencies(string userId);
        Task<List<ClientAgency>> GetAgencyClients(string userId);
        Task<ClientAgency> GetSingleClientAgencies(int Id);
        Task<ClientAgency> GetClientAgency(AgencyAlreadyExitModel agencyAlreadyExit);
        Task<List<ApplicationUser>> GetAgencyEmployee(string AgencyId);
        Task<List<ApplicationUser>> GetAllWorker();
    }
}
