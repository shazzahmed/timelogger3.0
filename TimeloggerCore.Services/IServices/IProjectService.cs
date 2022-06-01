using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Services.IService
{
    public interface IProjectService : IBaseService<ProjectModel, Project, int>
    {
        Task<BaseModel> GetProject(int projectId);
        Task<BaseModel> FreelancerProjects(string userId);
        Task<BaseModel> ProjectsWithCompanies();
        Task<BaseModel> AllProjects(string userRole);
        Task<BaseModel> GetUserProjects(string Id);
        Task<BaseModel> GetAgencyProjecList(string userId);
        Task<BaseModel> GetUserProjecList(string userId);
    }
}
