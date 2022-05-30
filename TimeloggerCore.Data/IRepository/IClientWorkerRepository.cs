using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using static TimeloggerCore.Common.Utility.Enums;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;

namespace TimeloggerCore.Data.IRepository
{
    public interface IClientWorkerRepository : IBaseRepository<ClientWorker, int>
    {
        Task<List<ClientWorker>> GetAllProjectWorker(string ProjectId, string clientId);
        Task<List<ClientWorker>> GetAllAgencyProjectWorker(ClientInviteModel clientInviteViewModel);
        Task<ClientWorker> DeleteAgencyProjectWorker(DeleteClientWorker deleteClientWorker);
        Task<List<ClientWorker>> GetProjectInvitation(string userId);
        Task<List<ClientWorker>> GetClientInvitation(string userId, WorkerType workerType);
        Task<List<ClientWorker>> GetAgencyInvitation(string AgencyId, WorkerType workerType);
        Task<List<ClientWorker>> GetWorkerInvitation(string AgencyId, int ProjectId, InvitationType invitation);
        Task<ClientWorker> AlreadyInvitationExit(int ProjectId, int ProjectInvitationId, string userId, WorkerType workerType);
        Task<ClientWorker> GetWorkerClientById(int Id);
        Task<List<ClientWorker>> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetAllIndividualWorker(string ProjectId, string clientId);
        Task<ClientWorker> AlreadySendInvitation(string userId, int invationId, int projectId, string AgencyId);
        Task<ClientWorker> GetClientWorker(int Id);
        Task<List<ClientWorker>> GetClientWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetAllInvitation(string SenderId);
        Task<List<ClientWorker>> GetAllClientWorker(string ClientId);
        Task<List<ClientWorker>> GetClientActiveProjects(string userId);
        Task<List<ClientWorker>> GetProjectInvitation(string workerId, WorkerType workerType);
        Task<List<ClientWorker>> GetUserProjecInviationtList(string Id, WorkerType invitationType);
    }
}
