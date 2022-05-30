using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using static TimeloggerCore.Common.Utility.Enums;
using System.Threading.Tasks;

namespace TimeloggerCore.Services
{
    public class ClientWorkerService : BaseService<ClientWorkerModel, ClientWorker, int>, IClientWorkerService
    {
        private readonly IClientWorkerRepository _clientWorkerRepository;

        public ClientWorkerService(IMapper mapper, IClientWorkerRepository clientWorkerRepository, IUnitOfWork unitOfWork) : base(mapper, clientWorkerRepository, unitOfWork)
        {
            this._clientWorkerRepository = clientWorkerRepository;
        }
        public Task<List<ClientWorker>> GetProjectInvitation(string workerId, WorkerType workerType)
        {
            return _clientWorkerRepository.GetProjectInvitation(workerId, workerType);
        }

        public Task<List<ClientWorker>> GetUserProjecInviationtList(string Id, WorkerType invitationType)
        {
            return _clientWorkerRepository.GetUserProjecInviationtList(Id, invitationType);
        }

    }
}
