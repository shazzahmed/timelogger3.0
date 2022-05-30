using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TimeloggerCore.Common.Models;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;
using ProjectsInvitation = TimeloggerCore.Data.Entities.ProjectsInvitation;

namespace TimeloggerCore.Data.Repository
{
    public class ClientWorkerRepository : BaseRepository<ClientWorker, int>, IClientWorkerRepository
    {
        public ClientWorkerRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<ClientWorker> AlreadyInvitationExit(int ProjectId, int ProjectInvitationId, string userId, WorkerType workerType)
        {
            var projectInvitation = await FirstOrDefaultAsync(
                 x => 
                 x.ProjectId == ProjectId
                 && x.ProjectsInvitationId == ProjectInvitationId
                 && x.WorkerId == userId
                 && !x.IsDeleted
                 && x.WorkerType == workerType
                 );
            return projectInvitation;
        }
        public async Task<List<ClientWorker>> GetAllProjectWorker(string ProjectId, string clientId)
        {
            int projectId = Convert.ToInt32(ProjectId);
            var clientWorker = await GetAsync(
                 x => x.ProjectId == projectId
                 && !x.IsDeleted
                 && x.ProjectsInvitation.AgencyId == clientId
                 && x.Worker.IsWorkerHasAgency
                 && x.WorkerType == WorkerType.AgencyWorker,
                 o => o.OrderBy(x=> x.Id),
                 i => i.Worker, i => i.Project, i => i.ProjectsInvitation, i => i.ProjectsInvitation.Agency);
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetAllAgencyProjectWorker(ClientInviteModel clientInviteViewModel)
        {
            int projectId = Convert.ToInt32(clientInviteViewModel.ProjectId);
            var agencyWorker = await GetAsync(
                 x => !x.IsDeleted
                 && x.ProjectId == projectId
                 && x.WorkerType == WorkerType.IndividualWorker
                 && x.ProjectsInvitation.AgencyId == clientInviteViewModel.UserId,
                 null,
                 i=>i.Worker, i=>i.ProjectsInvitation);
            return agencyWorker;
        }
        public async Task<ClientWorker> DeleteAgencyProjectWorker(DeleteClientWorker deleteClientWorker)
        {
            int projectId = Convert.ToInt32(deleteClientWorker.ProjectId);
            var clientWorker = await FirstOrDefaultAsync(
                 x =>
                 x.Id == deleteClientWorker.Id
                 );
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetProjectInvitation(string userId)
        {
            var workerInvitation = await GetAsync(
                 x =>
                 x.WorkerId == userId 
                 && x.WorkerType == WorkerType.ClientWorker,
                 null,
                 i => i.ProjectsInvitation.Project);
            return workerInvitation;
        }
        public async Task<List<ClientWorker>> GetClientInvitation(string userId, WorkerType workerType)
        {
            var workerInvitation = await GetAsync(
                 x =>
                 !x.IsDeleted && x.WorkerId == userId && x.WorkerType == workerType,
                 null,
                 i => i.Project.ApplicationUser);
            return workerInvitation;
        }
        public async Task<List<ClientWorker>> GetAgencyInvitation(string AgencyId, WorkerType workerType)
        {
            var workerInvitation = await GetAsync(
                 x =>
                 x.WorkerId == AgencyId && !x.IsDeleted && x.WorkerType == workerType,
                 null,
                 i => i.Project.ApplicationUser);
            return workerInvitation;
        }
        public async Task<List<ClientWorker>> GetWorkerInvitation(string AgencyId, int ProjectId, InvitationType invitationType)
        {
            var workerInvitation = await GetAsync(
                 x =>
                 x.ProjectsInvitation.AgencyId == AgencyId
                 && x.ProjectsInvitation.InvitationType == invitationType
                 && x.ProjectId == ProjectId
                 && !x.IsDeleted,
                 o => o.OrderBy(x => x.Id),
                 i => i.Project, i => i.Worker, i => i.ProjectsInvitation.Agency);
            return workerInvitation;
        }
        public async Task<ClientWorker> GetWorkerClientById(int Id)
        {
            var clientWorker = await FirstOrDefaultAsync(
                 x =>
                 x.Id == Id,
                 null,
                 i => i.Project, i => i.Worker, i => i.ProjectsInvitation.Agency);
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientAgencyWorker = await GetAsync(
                 x =>
                 !x.IsDeleted 
                 && x.IsAccepted 
                 && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId 
                 && x.Worker.AgencyId == clientAgencyWorkerViewModel.AgencyId,
                 o => o.OrderBy(x => x.Id),
                 i => i.Project, i => i.Worker, i => i.ProjectsInvitation.Agency);
            return clientAgencyWorker;
        }
        public async Task<List<ClientWorker>> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientAgencyWorker = await GetAsync(
                 x =>
                 !x.IsDeleted 
                 && x.IsAccepted 
                 && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId 
                 && x.Worker.AgencyId == clientAgencyWorkerViewModel.AgencyId,
                 o => o.OrderBy(x => x.Id),
                 i => i.Worker, i => i.ProjectsInvitation);
            return clientAgencyWorker;
        }
        public async Task<List<ClientWorker>> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientWorkerProject = await GetAsync(
                 x =>
                 x.ProjectsInvitationId == x.ProjectsInvitation.Id
                 && 
                 ((x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId)
                    || (x.WorkerId == clientAgencyWorkerViewModel.ClientId))
                 && !x.IsDeleted
                 && x.IsAccepted,
                 o => o.OrderBy(x => x.Id)
                 );
            return clientWorkerProject;
        }
        public async Task<List<ClientWorker>> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientWorkerProject = await GetAsync(
                 x =>
                 x.ProjectsInvitationId == x.ProjectsInvitation.Id
                 && ((
                    x.WorkerId == clientAgencyWorkerViewModel.MemberId 
                    && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId) 
                    || (x.WorkerId == clientAgencyWorkerViewModel.ClientId && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.MemberId))
                 && !x.IsDeleted
                 && x.IsAccepted,
                 o => o.OrderBy(x => x.Id)
                 );
            return clientWorkerProject;
        }
        public async Task<List<ClientWorker>> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientIndividualWorker = await GetAsync(
                 x =>
                 x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId
                 && !x.Worker.IsWorkerHasAgency
                 && !x.IsDeleted
                 && x.WorkerType == WorkerType.IndividualWorker,
                 o => o.OrderBy(x => x.Id)
                 );
            return clientIndividualWorker;
        }
        public async Task<List<ClientWorker>> GetClientWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientIndividualWorker = await GetAsync(
                 x =>
                 x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId
                && !x.Worker.IsWorkerHasAgency
                && !x.IsDeleted
                && x.WorkerType == WorkerType.IndividualWorker,
                 o => o.OrderBy(x => x.Id),
                 i => i.Worker, i => i.ProjectsInvitation);
            return clientIndividualWorker;
        }
        public async Task<List<ClientWorker>> GetAllIndividualWorker(string ProjectId, string clientId)
        {
            int projectId = Convert.ToInt32(ProjectId);
            var clientWorker = await GetAsync(
                 x =>
                 x.ProjectId == projectId
                 && !x.IsDeleted
                 && x.ProjectsInvitation.AgencyId == clientId
                 && !x.Worker.IsWorkerHasAgency
                 && x.WorkerType == WorkerType.IndividualWorker,
                 o => o.OrderBy(x => x.Id),
                 i => i.Worker, i => i.Project, i => i.ProjectsInvitation, i => i.ProjectsInvitation.Agency);
            return clientWorker;
        }
        public async Task<ClientWorker> AlreadySendInvitation(string userId, int invationId, int projectId, string AgencyId)
        {
            var clientWorker = await FirstOrDefaultAsync(
                 x =>
                 x.WorkerId == userId 
                 && x.ProjectsInvitationId == invationId 
                 && x.ProjectId == projectId 
                 && !x.IsDeleted 
                 && x.ProjectsInvitation.AgencyId == AgencyId,
                 null,
                 i => i.Project, i => i.Worker, i => i.ProjectsInvitation.Agency);
            return clientWorker;
        }
        public async Task<ClientWorker> GetClientWorker(int Id)
        {
            var clientWorker = await FirstOrDefaultAsync(
                 x =>
                 x.Id == Id,
                 null,
                 i => i.Project, i => i.Worker, i => i.ProjectsInvitation.Agency);
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetAllInvitation(string SenderId)
        {
            var clientWorker = await GetAsync(
                 x =>
                 x.ProjectsInvitation.AgencyId == SenderId 
                 && !x.IsDeleted && x.IsAccepted,
                 o => o.OrderBy(x => x.Id),
                 i => i.Worker);
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetAllClientWorker(string ClientId)
        {
            var clientWorker = await GetAsync(
                 x =>
                 x.ProjectsInvitation.AgencyId == ClientId
                 && !x.IsDeleted);
            return clientWorker;

        }
        public async Task<List<ClientWorker>> GetClientActiveProjects(string userId)
        {
            var invitations = await GetAsync(
                 x =>
                 x.ProjectsInvitation.AgencyId == userId && x.Status == MemberStatus.Active,
                 null,
                 i => i.Project, i => i.Worker, i => i.ProjectsInvitation);
            //var invitations = context.Invitations.Include(x=>x.Project).Include(x => x.User).Where(i => i.ClientID == userId && i.Status == MemberStatus.Active).ToList();
            return invitations;
        }
        public async Task<List<ClientWorker>> GetProjectInvitation(string workerId, WorkerType workerType)
        {
            var projectInvitation = await GetAsync(
                x => x.WorkerId == workerId && x.WorkerType == workerType && x.IsAccepted,
                null,
                i => i.Project, i => i.Project.ApplicationUser, i => i.Worker, i => i.ProjectsInvitation);
            return projectInvitation;
        }

        public async Task<List<ClientWorker>> GetUserProjecInviationtList(string Id, WorkerType invitationType)
        {
            //List<ClientWorker> projectInvitation = new List<ClientWorker>();
            var projectInvitation = await GetAsync(
                x => 
                x.ProjectsInvitation.AgencyId == Id 
                && (x.WorkerType == invitationType || x.WorkerType == WorkerType.AgencyWorker)
                && x.IsAccepted,
                null,
                i => i.ProjectsInvitation, i => i.Worker);
            return projectInvitation;
        }
    }
}
