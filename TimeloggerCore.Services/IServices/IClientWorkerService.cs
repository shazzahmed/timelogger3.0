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
        Task<BaseModel> GetProjectInvitation(string workerId, WorkerType workerType);
        Task<BaseModel> GetUserProjecInviationtList(string Id, WorkerType invitationType);
        Task<BaseModel> AlreadyInvitationExit(int ProjectId, int ProjectInvitationId, string userId, WorkerType workerType);
        Task<BaseModel> GetAllProjectWorker(string ProjectId, string clientId);
        Task<BaseModel> GetAllAgencyProjectWorker(ClientInviteModel clientInviteViewModel);
        Task<BaseModel> DeleteAgencyProjectWorker(DeleteClientWorker deleteClientWorker);
        Task<BaseModel> GetProjectInvitation(string userId);
        Task<BaseModel> GetClientInvitation(string userId, WorkerType workerType);
        Task<BaseModel> GetAgencyInvitation(string AgencyId, WorkerType workerType);
        Task<BaseModel> GetWorkerInvitation(string AgencyId, int ProjectId, InvitationType invitationType);
        Task<BaseModel> GetWorkerClientById(int Id);
        Task<BaseModel> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerModel);

        Task<BaseModel> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerModel);
        Task<BaseModel> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerModel);
        Task<BaseModel> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerModel);
        Task<BaseModel> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerModel);
        Task<BaseModel> GetClientWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerModel);
        Task<BaseModel> GetAllIndividualWorker(string ProjectId, string clientId);
        Task<BaseModel> AlreadySendInvitation(string userId, int invationId, int projectId, string AgencyId);
        Task<BaseModel> GetClientWorker(int Id);
        Task<BaseModel> GetAllInvitation(string SenderId);
        Task<BaseModel> GetAllClientWorker(string ClientId);
        Task<BaseModel> GetClientActiveProjects(string userId);
    }
}
