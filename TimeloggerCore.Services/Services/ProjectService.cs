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
using System.Linq;

namespace TimeloggerCore.Services
{
    public class ProjectService : BaseService<ProjectModel, Project, int>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientWorkerService _clientWorkerService;

        public ProjectService(
            IMapper mapper,
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork, 
            IClientWorkerService clientWorkerService
            ) : base(mapper, projectRepository, unitOfWork)
        {
            _projectRepository = projectRepository;
            _clientWorkerService = clientWorkerService;
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
        public async Task<BaseModel> ProjectsWithInviation(UserProjectViewModel userProjectViewModel)
        {
            var projectWithInvitationViewModel = new ProjectWithInvitationModel();
            var userProject = await _projectRepository.GetUserProjecList(userProjectViewModel.UserId);
            var projectInviation = (List<ClientWorkerModel>)(await _clientWorkerService.GetProjectInvitation(userProjectViewModel.UserId, userProjectViewModel.WorkerInvitationType)).Data;
            projectWithInvitationViewModel.OwnProject = mapper.Map<List<Project>, List<ProjectModel>>(userProject);
            projectWithInvitationViewModel.ProjectInviation = projectInviation;
            if (projectInviation != null)
            {
                projectWithInvitationViewModel.UserProjectInvitation = projectInviation.Where(x => x.IsAccepted && !x.IsDeleted).Select(x => x.Project).ToList();
            }
            return new BaseModel
            {
                Success = true,
                Data = projectWithInvitationViewModel
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
