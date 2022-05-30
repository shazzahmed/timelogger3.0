using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.IRepository
{
    public interface IProjectRepository : IBaseRepository<Project, int>
    {
        Task<Project> GetProject(int projectId);
        Task<List<Project>> FreelancerProjects(string userId);
        Task<List<Project>> ProjectsWithCompanies();
        Task<List<Project>> AllProjects(string userRole);
        Task<Project> GetUserProjects(string Id);
        Task<List<Project>> GetAgencyProjecList(string userId);
        Task<List<Project>> GetUserProjecList(string userId);
    }
}
