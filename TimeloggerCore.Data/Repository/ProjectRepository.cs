using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Repository
{
    public class ProjectRepository : BaseRepository<Project, int>, IProjectRepository
    {
        public ProjectRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<Project>> FreelancerProjects(string userId)
        {
            var projects = await DbContext.Project.Where(p => p.UserId == userId).ToListAsync();
            return projects;
        }

        public async Task<List<Project>> AllProjects(string userRole)
        {
            var projects = DbContext.Project.Include(x => x.Invitations);
            if (userRole.Equals("Client"))
                //projects = projects.Include(p => p.Invitations.Select(i => i.User));
                projects = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Project, List<Invitation>>)projects.Include(p => p.Invitations.Select(i => i.User));
            else if (userRole.Equals("Freelancer"))
                //projects = projects.Include(p => p.Invitations.Select(i => i.Client));
                projects = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Project, List<Invitation>>)projects.Include(p => p.Invitations.Select(i => i.Client));
            return await projects.ToListAsync();
        }


        public async Task<List<Project>> ProjectsWithCompanies()
        {
            List<Project> project = new List<Project>();
            try
            {
                //project= await context.Project.Include(p => p.Invitations).Include(x => x.ApplicationUser).ToListAsync();
                project = await DbContext.Project.Include(p => p.Invitations).Include(x => x.ApplicationUser).ToListAsync();

            }
            catch (Exception ex)
            {

            }
            return project;
        }
        public async Task<Project> GetUserProjects(string Id)
        {
            int projectId = Convert.ToInt32(Id);
            return await DbContext.Project.Where(x => x.Id == projectId).Include(x => x.ApplicationUser).FirstOrDefaultAsync();
        }
        public async Task<Project> GetUserProjectList(string userId)
        {
            var project = await DbContext.Project.Where(x => x.UserId == userId && !x.IsDeleted).Include(x => x.ApplicationUser).FirstOrDefaultAsync();
            return project;
        }

        public async Task<List<Project>> GetUserProjecList(string userId)
        {
            var project = await DbContext.Project.Where(x =>
            x.UserId == userId
            || x.UserId == null
            && !x.IsDeleted).Include(x => x.ApplicationUser).ToListAsync();
            return project;
        }

        public async Task<List<Project>> GetAgencyProjecList(string userId)
        {
            var projects = new List<Project>();
            try
            {
                //var projects = await context.ClientAgency.Where(x =>
                //         x.AgencyId == userId)
                //    .ToListAsync();

                var projects1 = (await (from CA in DbContext.ClientAgency
                                        join U in DbContext.User on CA.AgencyId equals U.AgencyId
                                        join CW in DbContext.ClientWorker on U.Id equals CW.WorkerId
                                        where U.AgencyId == userId && U.IsWorkerHasAgency
                                        select new
                                        {
                                            Title = CW.Project.Title,
                                            Description = CW.Project.Description,
                                        }).Distinct().ToListAsync()).Select(x => new Project
                                        {
                                            Description = x.Description,
                                            Title = x.Title
                                        }).ToList();


                return projects1;

                //var q = s.GroupBy(c =>new { x = c.Client.Email, z=c.Project.Id }).Distinct();
                //return new List<ProjectsInvitation>();
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<List<ClientWorker>> GetProjectInvitation(string workerId, WorkerType workerType)
        {
            List<ClientWorker> projectInvitation = new List<ClientWorker>();
            projectInvitation = await DbContext.ClientWorker.Where(x => x.WorkerId == workerId && x.WorkerType == workerType && x.IsAccepted)
                                      .Include(x => x.Project).Include(x => x.Project.ApplicationUser).Include(x => x.Worker).Include(x => x.ProjectsInvitation).ToListAsync();
            return projectInvitation;
        }

        public async Task<List<ClientWorker>> GetUserProjecInviationtList(string Id, WorkerType invitationType)
        {
            List<ClientWorker> projectInvitation = new List<ClientWorker>();
            projectInvitation = await DbContext.ClientWorker.Where(x => x.ProjectsInvitation.AgencyId == Id && (x.WorkerType == invitationType
            || x.WorkerType == WorkerType.AgencyWorker)
            && x.IsAccepted).Include(x => x.ProjectsInvitation).Include(x => x.Worker).ToListAsync();
            return projectInvitation;
        }

        public Task<int> PostProject(Project project)
        {
            //var config = new MapperConfiguration(cfg =>
            //        cfg.CreateMap<Project, ProjectViewModel>()
            //        .ReverseMap()
            //    );
            //var mapper = new Mapper(config);
            //Insert(mapper.Map<Project>(project));
            AddAsync(project);
            return DbContext.SaveChangesAsync();
            //return SaveChangesAsync();
        }

        //Task<List<Project>> IProjectRepository.GetAgencyProjecList(string userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
