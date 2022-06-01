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

        public ProjectService(
            IMapper mapper, 
            IProjectRepository projectRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, projectRepository, unitOfWork)
        {
            _projectRepository = projectRepository;
        }
        public async Task<BaseModel> GetProject(int projectId)
        {
            var result = await _projectRepository.GetProject(projectId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Project, ProjectModel>(result)
            };
        }
        public async Task<BaseModel> AllProjects(string userRole)
        {
            var result = await _projectRepository.AllProjects(userRole);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Project>, List<ProjectModel>>(result)
            };
        }
        public async Task<BaseModel> FreelancerProjects(string userId)
        {
            var result = await _projectRepository.FreelancerProjects(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Project>, List<ProjectModel>>(result)
            };
        }
        public async Task<BaseModel> GetAgencyProjecList(string userId)
        {
            var result = await _projectRepository.GetAgencyProjecList(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Project>, List<ProjectModel>>(result)
            };
        }
        public async Task<BaseModel> GetUserProjecList(string userId)
        {
            var result = await _projectRepository.GetUserProjecList(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Project>, List<ProjectModel>>(result)
            };
        }
        public async Task<BaseModel> GetUserProjects(string Id)
        {
            var result = await _projectRepository.GetUserProjects(Id);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Project, ProjectModel>(result)
            };
        }
        public async Task<BaseModel> ProjectsWithCompanies()
        {
            var result = await _projectRepository.ProjectsWithCompanies();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Project>, List<ProjectModel>>(result)
            };
        }
    }
}
