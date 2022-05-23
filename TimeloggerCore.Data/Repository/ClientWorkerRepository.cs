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
        public async Task<ProjectsInvitation> ProjectAssign(string ProjectId, string AgencyId, InvitationType invitationType)
        {
            int projectId = Convert.ToUInt16(ProjectId);
            var projectInvitation = await DbContext.ProjectsInvitations.Where(x => x.AgencyId == AgencyId
            && x.ProjectId == projectId
            && x.InvitationType == invitationType
            && !x.IsDeleted
            ).Include(x => x.Agency).FirstOrDefaultAsync();
            return projectInvitation;
        }

        public async Task<ClientWorker> AlreadyInvitationExit(int ProjectId, int ProjectInvitationId, string userId, WorkerType workerType)
        {
            var projectInvitation = await DbContext.ClientWorker.Where(x =>
            x.ProjectId == ProjectId
            && x.ProjectsInvitationId == ProjectInvitationId
            && x.WorkerId == userId
            && !x.IsDeleted
          //  && x.WorkerType == WorkerType.WorkerClient
          && x.WorkerType == workerType
            ).FirstOrDefaultAsync();
            return projectInvitation;
        }
        public async Task<List<ClientWorker>> GetAllProjectWorker(string ProjectId, string clientId)
        {
            int projectId = Convert.ToInt32(ProjectId);
            var clientWorker = await DbContext.ClientWorker.Where(x => x.ProjectId == projectId
            && !x.IsDeleted
            && x.ProjectsInvitation.AgencyId == clientId
            && x.Worker.IsWorkerHasAgency
            && x.WorkerType == WorkerType.AgencyWorker
            ).Include(x => x.Worker).Include(x => x.Project).Include(x => x.ProjectsInvitation).Include(x => x.ProjectsInvitation.Agency).ToListAsync();
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetAllAgencyProjectWorker(ClientInviteModel clientInviteViewModel)
        {
            int projectId = Convert.ToInt32(clientInviteViewModel.ProjectId);
            var agencyWorker = await DbContext.ClientWorker.Where(x =>
            !x.IsDeleted
            && x.ProjectId == projectId
            && x.WorkerType == WorkerType.IndividualWorker
            && x.ProjectsInvitation.AgencyId == clientInviteViewModel.UserId

            // && ((!clientInviteViewModel.IsAgencyWorker) || x.ProjectsInvitation.AgencyId == clientInviteViewModel.UserId)
            ).Include(x => x.Worker).Include(x => x.ProjectsInvitation).ToListAsync();
            return agencyWorker;
        }
        public async Task<ClientWorker> DeleteAgencyProjectWorker(DeleteClientWorker deleteClientWorker)
        {
            int projectId = Convert.ToInt32(deleteClientWorker.ProjectId);
            var clientWorker = await DbContext.ClientWorker.Where(x => x.Id == deleteClientWorker.Id).FirstOrDefaultAsync();
            return clientWorker;
        }
        public Task<List<ClientWorker>> GetProjectInvitation(string userId)
        {
            var workerInvitation = DbContext.ClientWorker.Where(x => x.WorkerId == userId && x.WorkerType == WorkerType.ClientWorker).Include(x => x.ProjectsInvitation.Project).ToListAsync();
            return workerInvitation;
        }

        public Task<Entities.ApplicationUser> GetCurrentUser(string userId)
        {
            var user = DbContext.User.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user;
        }
        public async Task<List<ClientWorker>> GetClientInvitation(string userId, WorkerType workerType)
        {
            var workerInvitation = await DbContext.ClientWorker.Where(x => !x.IsDeleted && x.WorkerId == userId && x.WorkerType == workerType).Include(x => x.Project.ApplicationUser).ToListAsync();
            return workerInvitation;
        }
        public async Task<List<ClientWorker>> GetAgencyInvitation(string AgencyId, WorkerType workerType)
        {
            var workerInvitation = await DbContext.ClientWorker.Where(x => x.WorkerId == AgencyId && !x.IsDeleted && x.WorkerType == workerType).Include(x => x.Project.ApplicationUser).ToListAsync();
            return workerInvitation;
        }
        public async Task<List<ClientWorker>> GetWorkerInvitation(string AgencyId, int ProjectId, InvitationType invitationType)
        {
            var workerInvitation = await DbContext.ClientWorker.Where(x =>
             x.ProjectsInvitation.AgencyId == AgencyId
             && x.ProjectsInvitation.InvitationType == invitationType
             && x.ProjectId == ProjectId
             && !x.IsDeleted
            ).Include(x => x.Project).Include(x => x.Worker).Include(x => x.ProjectsInvitation.Agency).ToListAsync();
            return workerInvitation;
        }
        public async Task<Entities.ApplicationUser> SearchUserByEmail(string email)
        {
            Entities.ApplicationUser userExit = await DbContext.User.Where(x => x.Email == email).FirstOrDefaultAsync();
            return userExit;

        }
        public async Task<Project> GetProject(int projectId)
        {
            var project = await DbContext.Project.Where(x => x.Id == projectId).FirstOrDefaultAsync();
            return project;
        }
        public async Task<ClientWorker> GetWorkerClientById(int Id)
        {
            var clientWorker = await DbContext.ClientWorker.Where(x => x.Id == Id)
                .Include(x => x.Project)
                .Include(x => x.Worker)
                .Include(x => x.ProjectsInvitation.Agency)
                .FirstOrDefaultAsync();
            return clientWorker;
        }
        public async Task<List<ApplicationUser>> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientAgencyWorker = await DbContext.ClientWorker.Where(x => !x.IsDeleted && x.IsAccepted && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId && x.Worker.AgencyId == clientAgencyWorkerViewModel.AgencyId).Select(x => x.Worker).ToListAsync();
            return clientAgencyWorker;
        }
        public async Task<List<ClientWorker>> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientAgencyWorker = await DbContext.ClientWorker.Where(x => !x.IsDeleted && x.IsAccepted && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId && x.Worker.AgencyId == clientAgencyWorkerViewModel.AgencyId).Include(x => x.Worker).Include(x => x.ProjectsInvitation).ToListAsync();
            return clientAgencyWorker;
        }
        public async Task<List<Project>> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientWorkerProject = await DbContext.ClientWorker.Where(x =>
            x.ProjectsInvitationId == x.ProjectsInvitation.Id
            && ((
                 x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId
                ) ||
                (
                x.WorkerId == clientAgencyWorkerViewModel.ClientId
                )
                )
            && !x.IsDeleted
            && x.IsAccepted
            ).Select(x => x.Project).ToListAsync();
            return clientWorkerProject;
        }
        public async Task<List<Project>> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientWorkerProject = await DbContext.ClientWorker.Where(x =>
            x.ProjectsInvitationId == x.ProjectsInvitation.Id
            && ((x.WorkerId == clientAgencyWorkerViewModel.MemberId &&
                 x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId
                ) ||
                (
                x.WorkerId == clientAgencyWorkerViewModel.ClientId
                && x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.MemberId)
                )
            && !x.IsDeleted
            && x.IsAccepted
            ).Select(x => x.Project).ToListAsync();
            return clientWorkerProject;
        }
        public async Task<List<ApplicationUser>> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientIndividualWorker = await DbContext.ClientWorker.Where(x => x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId
            && !x.Worker.IsWorkerHasAgency
            && !x.IsDeleted
            && x.WorkerType == WorkerType.IndividualWorker
            ).Select(x => x.Worker).ToListAsync();

            return clientIndividualWorker;
        }
        public async Task<List<ClientWorker>> GetClientWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var clientIndividualWorker = await DbContext.ClientWorker.Where(x =>
            x.ProjectsInvitation.AgencyId == clientAgencyWorkerViewModel.ClientId
            && !x.Worker.IsWorkerHasAgency
            && !x.IsDeleted
            && x.WorkerType == WorkerType.IndividualWorker
            ).Include(x => x.Worker).Include(x => x.ProjectsInvitation).ToListAsync();

            return clientIndividualWorker;
        }
        public async Task<List<ClientWorker>> GetAllIndividualWorker(string ProjectId, string clientId)
        {
            int projectId = Convert.ToInt32(ProjectId);
            var clientWorker = await DbContext.ClientWorker.Where(x => x.ProjectId == projectId
            && !x.IsDeleted
            && x.ProjectsInvitation.AgencyId == clientId
            && !x.Worker.IsWorkerHasAgency
            && x.WorkerType == WorkerType.IndividualWorker
            ).Include(x => x.Worker).Include(x => x.Project).Include(x => x.ProjectsInvitation).Include(x => x.ProjectsInvitation.Agency).ToListAsync();
            return clientWorker;
        }
        public async Task<ClientWorker> AlreadySendInvitation(string userId, int invationId, int projectId, string AgencyId)
        {
            var clientWorker = await DbContext.ClientWorker.Where(x => x.WorkerId == userId && x.ProjectsInvitationId == invationId && x.ProjectId == projectId && !x.IsDeleted && x.ProjectsInvitation.AgencyId == AgencyId).Include(x => x.Worker).Include(x => x.ProjectsInvitation.Agency).FirstOrDefaultAsync();
            return clientWorker;
        }
        public async Task<ClientWorker> GetClientWorker(int Id)
        {
            var clientWorker = await DbContext.ClientWorker.Where(x => x.Id == Id).Include(x => x.Worker).Include(x => x.ProjectsInvitation.Agency).FirstOrDefaultAsync();
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetAllInvitation(string SenderId)
        {
            var clientWorker = await DbContext.ClientWorker.Where(x => x.ProjectsInvitation.AgencyId == SenderId && !x.IsDeleted && x.IsAccepted).Include(x => x.Worker).ToListAsync();
            return clientWorker;
        }
        public async Task<List<ClientWorker>> GetAllClientWorker(string ClientId)
        {
            var clientWorker = await DbContext.ClientWorker.Where(x => x.ProjectsInvitation.AgencyId == ClientId
            && !x.IsDeleted).ToListAsync();
            return clientWorker;

        }
    }
}
