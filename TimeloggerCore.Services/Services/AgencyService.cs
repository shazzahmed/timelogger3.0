using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;

namespace TimeloggerCore.Services
{
    public class AgencyService : BaseService<ClientAgencyModel, ClientAgency, int>, IAgencyService
    {
        private readonly IClientAgencyRepository _clientAgencyRepository;

        public AgencyService(IMapper mapper, IClientAgencyRepository clientAgencyRepository, IUnitOfWork unitOfWork) : base(mapper, clientAgencyRepository, unitOfWork)
        {
            _clientAgencyRepository = clientAgencyRepository;
        }
        public Task<List<ClientAgency>> GetAgencyClients(string userId)
        {
            return _clientAgencyRepository.GetAgencyClients(userId);
        }

        public Task<List<ApplicationUser>> GetAgencyEmployee(string AgencyId)
        {
            return _clientAgencyRepository.GetAgencyEmployee(AgencyId);
        }

        public Task<List<ApplicationUser>> GetAllWorker()
        {
            return _clientAgencyRepository.GetAllWorker();
        }

        public Task<List<ClientAgency>> GetClientAgencies(string userId)
        {
            return _clientAgencyRepository.GetClientAgencies(userId);
        }

        public Task<ClientAgency> GetClientAgency(AgencyAlreadyExitModel agencyAlreadyExit)
        {
            return _clientAgencyRepository.GetClientAgency(agencyAlreadyExit);
        }

        public Task<ClientAgency> GetSingleClientAgencies(int Id)
        {
            return _clientAgencyRepository.GetSingleClientAgencies(Id);
        }
    }
}
