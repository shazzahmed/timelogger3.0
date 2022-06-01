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
        Task<BaseModel> GetClientAgencies(string userId);
        Task<BaseModel> GetAgencyClients(string userId);
        Task<BaseModel> GetSingleClientAgencies(string userId);
        Task<BaseModel> GetClientAgency(ClientAgencyModel clientAgencyModel);
        Task<BaseModel> GetAgencyEmployee(string AgencyId);
        Task<BaseModel> GetAllWorker();
        Task<BaseModel> AddClientAgency(ClientAgencyModel clientAgencyModel);
        Task<BaseModel> ConfirmAgency(string AgencyId);
        Task<BaseModel> ConfirmWorker(string AgencyId);
    }
}
