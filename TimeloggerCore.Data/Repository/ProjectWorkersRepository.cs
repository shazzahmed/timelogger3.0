using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Repository
{
    public class ProjectWorkersRepository : BaseRepository<ProjectWorkers, int>, IProjectWorkersRepository
    {
        public ProjectWorkersRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<ProjectWorkersModel>> GetProjectWorkers(int ProjectId)
        {
            var projectWorkersViewModel = new List<ProjectWorkersModel>();

            var a = await DbContext.ProjectWorkers.Include(x => x.ProjectInvitations.InvitationRequest)
                  .Where(x => x.ProjectInvitations.ProjectId == ProjectId).Select(y => new { y.Worker, y.ProjectInvitations, y.ProjectInvitations.InvitationRequest }).ToListAsync();
            foreach (var item in a)
            {
                var projectWorkersViewModel1 = new ProjectWorkersModel();
                projectWorkersViewModel1.CanAddManualTime = false;
                projectWorkersViewModel1.CanEditTimeLog = false;
                //projectWorkersViewModel1.CreatedOn = item.ProjectInvitations.CreatedAt;
                projectWorkersViewModel1.EmailAddress = null;
                projectWorkersViewModel1.ExistingUser = item.InvitationRequest.ExistingUser;
                projectWorkersViewModel1.Id = item.ProjectInvitations.Id;
                projectWorkersViewModel1.IsAccepted = item.ProjectInvitations.IsAccepted;
                projectWorkersViewModel1.IsActive = item.ProjectInvitations.IsAccepted;
                projectWorkersViewModel1.IsDeleted = item.ProjectInvitations.IsDeleted;
                projectWorkersViewModel1.MaximumHours = 0;
                projectWorkersViewModel1.MinimumHours = 0;
                //projectWorkersViewModel1.Project = item.ProjectInvitations.Project;
                projectWorkersViewModel1.ProjectHours = 0;
                projectWorkersViewModel1.ProjectId = item.ProjectInvitations.ProjectId;
                //projectWorkersViewModel1.ProjectsInvitation = item.ProjectInvitations.Project;
                projectWorkersViewModel1.ProjectsInvitationId = item.ProjectInvitations.InvitationRequestId;
                projectWorkersViewModel1.RatePerHour = 0;
                projectWorkersViewModel1.Status = item.ProjectInvitations.Status;
                //projectWorkersViewModel1.Worker = item.Worker;
                projectWorkersViewModel1.WorkerId = item.Worker.Id;
                projectWorkersViewModel1.WorkerType = WorkerType.IndividualWorker;
                projectWorkersViewModel.Add(projectWorkersViewModel1);
                //projectWorkersViewModel.Add(projectWorkersViewModel1);
            }
            //a.ForEach(t=>{
            //    projectWorkersViewModel1.CanAddManualTime = false;
            //    projectWorkersViewModel1.CanEditTimeLog = false;
            //    projectWorkersViewModel1.CreatedOn = t.ProjectInvitations.CreatedOn;
            //    projectWorkersViewModel1.EmailAddress = null;
            //    projectWorkersViewModel1.ExistingUser = t.InvitationRequest.ExistingUser;
            //    projectWorkersViewModel1.Id = t.ProjectInvitations.Id;
            //    projectWorkersViewModel1.IsAccepted = t.ProjectInvitations.IsAccepted;
            //    projectWorkersViewModel1.IsActive = t.ProjectInvitations.IsAccepted;
            //    projectWorkersViewModel1.IsDeleted = t.ProjectInvitations.IsAccepted;
            //    projectWorkersViewModel1.MaximumHours = 0;
            //    projectWorkersViewModel1.MinimumHours = 0;
            //    projectWorkersViewModel1.Project = t.ProjectInvitations.Project;
            //    projectWorkersViewModel1.ProjectHours = 0;
            //    projectWorkersViewModel1.ProjectId = t.ProjectInvitations.ProjectId;
            //    projectWorkersViewModel1.ProjectsInvitation = t.ProjectInvitations.Project;
            //    projectWorkersViewModel1.ProjectsInvitationId = t.ProjectInvitations.InvitationRequestId;
            //    projectWorkersViewModel1.RatePerHour = 0;
            //    projectWorkersViewModel1.Status = t.ProjectInvitations.Status;
            //    projectWorkersViewModel1.Worker = t.Worker;
            //    projectWorkersViewModel1.WorkerId = t.Worker.Id;
            //    projectWorkersViewModel1.WorkerType = WorkerType.IndividualWorker;
            //    projectWorkersViewModel.Add(projectWorkersViewModel1);
            //});
            return projectWorkersViewModel;
        }
    }
}
