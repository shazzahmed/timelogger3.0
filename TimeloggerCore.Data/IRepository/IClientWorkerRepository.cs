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
        Task<Entities.ProjectsInvitation> ProjectAssign(string ProjectId, string AgencyId, InvitationType invitationType);
        Task<List<ClientWorker>> GetAllProjectWorker(string ProjectId, string clientId);
        Task<List<ClientWorker>> GetAllAgencyProjectWorker(ClientInviteModel clientInviteViewModel);
        Task<ClientWorker> DeleteAgencyProjectWorker(DeleteClientWorker deleteClientWorker);
        Task<List<ClientWorker>> GetProjectInvitation(string userId);
        Task<ApplicationUser> GetCurrentUser(string userId);
        Task<List<ClientWorker>> GetClientInvitation(string userId, WorkerType workerType);
        Task<List<ClientWorker>> GetAgencyInvitation(string AgencyId, WorkerType workerType);
        Task<List<ClientWorker>> GetWorkerInvitation(string AgencyId, int ProjectId, InvitationType invitation);
        Task<ClientWorker> AlreadyInvitationExit(int ProjectId, int ProjectInvitationId, string userId, WorkerType workerType);
        Task<ApplicationUser> SearchUserByEmail(string email);
        Task<Project> GetProject(int projectId);
        Task<ClientWorker> GetWorkerClientById(int Id);
        Task<List<ApplicationUser>> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<Project>> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<Project>> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ApplicationUser>> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetAllIndividualWorker(string ProjectId, string clientId);
        Task<ClientWorker> AlreadySendInvitation(string userId, int invationId, int projectId, string AgencyId);
        Task<ClientWorker> GetClientWorker(int Id);
        Task<List<ClientWorker>> GetClientWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel);
        Task<List<ClientWorker>> GetAllInvitation(string SenderId);
        Task<List<ClientWorker>> GetAllClientWorker(string ClientId);
    }
}
