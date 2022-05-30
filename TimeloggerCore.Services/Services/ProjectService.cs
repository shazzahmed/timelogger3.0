using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Services
{
    public class ProjectService : BaseService<ProjectModel, Project, int>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository, IUnitOfWork unitOfWork) : base(mapper, projectRepository, unitOfWork)
        {
            _projectRepository = projectRepository;
        }
        public Task<List<Project>> AllProjects(string userRole)
        {
            return _projectRepository.AllProjects(userRole);
        }

        public Task<List<Project>> FreelancerProjects(string userId)
        {
            return _projectRepository.FreelancerProjects(userId);
        }

        public Task<List<Project>> GetAgencyProjecList(string userId)
        {
            return _projectRepository.GetAgencyProjecList(userId);
        }
        public Task<List<Project>> GetUserProjecList(string userId)
        {
            return _projectRepository.GetUserProjecList(userId);
        }

        public Task<Project> GetUserProjects(string Id)
        {
            return _projectRepository.GetUserProjects(Id);
        }

        public Task<List<Project>> ProjectsWithCompanies()
        {
            return _projectRepository.ProjectsWithCompanies();
        }
    }
}
