using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using static TimeloggerCore.Common.Utility.Enums;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IClientWorkerService : IBaseService<ClientWorkerModel, ClientWorker, int>
    {
        Task<List<ClientWorker>> GetProjectInvitation(string workerId, WorkerType workerType);
        Task<List<ClientWorker>> GetUserProjecInviationtList(string Id, WorkerType invitationType);
    }
}
