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
        public async Task<Project> GetProject(int projectId)
        {
            var project = await FirstOrDefaultAsync(x => x.Id == projectId);
            return project;
        }
        public async Task<List<Project>> FreelancerProjects(string userId)
        {
            var projects = await GetAsync(p => p.UserId == userId);
            return projects;
        }

        public async Task<List<Project>> AllProjects(string userRole)
        {
            var projects = Get().Include(x => x.Invitations);
            if (userRole.Equals("Client"))
                projects.Include(p => p.Invitations.Select(i => i.User));
            if (userRole.Equals("Freelancer"))
                projects.Include(p => p.Invitations.Select(i => i.Client));

            //var projects = DbContext.Project.Include(x => x.Invitations);
            //if (userRole.Equals("Client"))
            //    //projects = projects.Include(p => p.Invitations.Select(i => i.User));
            //    projects = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Project, List<Invitation>>)projects.Include(p => p.Invitations.Select(i => i.User));
            //else if (userRole.Equals("Freelancer"))
            //    //projects = projects.Include(p => p.Invitations.Select(i => i.Client));
            //    projects = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Project, List<Invitation>>)projects.Include(p => p.Invitations.Select(i => i.Client));
            return await projects.ToListAsync();
        }


        public async Task<List<Project>> ProjectsWithCompanies()
        {
            var project = await GetAsync(null,null,i => i.Invitations,i => i.ApplicationUser);
            return project;
        }
        public async Task<Project> GetUserProjects(string Id)
        {
            int projectId = Convert.ToInt32(Id);
            return await FirstOrDefaultAsync(x => x.Id == projectId,
                null,
                i => i.ApplicationUser);
        }
        public async Task<Project> GetUserProjectList(string userId)
        {
            var project = await FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted, 
                null, 
                i => i.ApplicationUser);
            return project;
        }

        public async Task<List<Project>> GetUserProjecList(string userId)
        {
            var project = await GetAsync(
            x =>
            x.UserId == userId
            || x.UserId == null
            && !x.IsDeleted,
            null,
            x => x.ApplicationUser);
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

        //Task<List<Project>> IProjectRepository.GetAgencyProjecList(string userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
