using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Core.ISecurity;
using TimeloggerCore.Services.IService;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.RestApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IAgencyService _agencyService;
        private readonly IClientWorkerService _clientWorkerService;
        private readonly IInvitationRequestService _invitationRequestService;
        private readonly IWorkerService _workerService;
        private readonly ISecurityService _securityService;

        public ProjectsController(
            IProjectService projectService,
            IAgencyService agencyService,
            IClientWorkerService clientWorkerService,
            IInvitationRequestService invitationRequestService,
            IWorkerService workerService, 
            ISecurityService securityService)
        {
            _projectService = projectService;
            _agencyService = agencyService;
            _clientWorkerService = clientWorkerService;
            _invitationRequestService = invitationRequestService;
            _workerService = workerService;
            _securityService = securityService;
        }
        // GET: Api/Projects/GetProject
        [HttpGet]
        [ActionName("GetProject")]
        [Route("GetProject")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetProject()
        {
            try
            {
                var result = await _projectService.Get();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/GetProject/{id}
        [HttpGet]
        [ActionName("GetProject")]
        [Route("GetProject/{id}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var result = await _projectService.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return NotFound();
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/WithCompanies
        [HttpGet]
        [ActionName("GetProjectsWithCompanies")]
        [Route("WithCompanies")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetProjectsWithCompanies()
        {
            try
            {
                var result = await _projectService.ProjectsWithCompanies();
                if (result == null)
                {
                    return NotFound();
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/WithInvitation
        [HttpGet]
        [ActionName("ProjectsWithInviation")]
        [Route("WithInvitation")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ProjectsWithInviation(UserProjectViewModel userProjectViewModel)
        {
            try
            {
                var result = await _projectService.ProjectsWithInviation(userProjectViewModel);
                if (result == null)
                {
                    return NotFound();
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/UserProjects
        [HttpGet]
        [ActionName("UserProjects")]
        [Route("UserProjects")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> UserProjects(ClientInviteModel clientInviteViewModel)
        {
            try
            {
                var invitationRequest = new InvitationRequestModel();
                invitationRequest.FromUserId = clientInviteViewModel.UserId;
                var clientAgencies = (List<ClientAgencyModel>)(await _agencyService.GetClientAgencies(clientInviteViewModel.UserId)).Data;
                var result = new ClientInviteAgencyModel
                {
                    //Project = await _projectRepository.GetUserProjects(clientInviteViewModel.ProjectId),
                    Project = (ProjectModel)(await _projectService.GetUserProjects(clientInviteViewModel.ProjectId)).Data,
                    ClientAgencies = clientAgencies.Where(x => x.IsAgencyAccepted).ToList(),
                    IndividualWorker = (List<ClientWorkerModel>)(await _clientWorkerService.GetAllIndividualWorker(clientInviteViewModel.ProjectId, clientInviteViewModel.UserId)).Data,
                    ClientWorker = (List<ClientWorkerModel>)(await _clientWorkerService.GetAllProjectWorker(clientInviteViewModel.ProjectId, clientInviteViewModel.UserId)).Data,
                    InvitationRequest = (List<InvitationRequestModel>)(await _invitationRequestService.GetClientAgencies(invitationRequest)).Data,
                    projectWorkers = (List<ProjectWorkersModel>)(await _workerService.GetProjectWorkers(Convert.ToInt32(clientInviteViewModel.ProjectId))).Data
                };
                
                if (result == null)
                {
                    return NotFound();
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/GetProjectAgencyWorker
        [HttpGet]
        [ActionName("GetFreelancerProjects")]
        [Route("GetProjectAgencyWorker")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetFreelancerProjects(ClientInviteModel clientInviteViewModel)
        {
            try
            {
                var result = await _clientWorkerService.GetAllProjectWorker(clientInviteViewModel.ProjectId, clientInviteViewModel.UserId);
                if (result == null)
                {
                    return NotFound();
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/FreelancerProjects/{userId}
        [HttpGet]
        [ActionName("GetFreelancerProjects")]
        [Route("FreelancerProjects/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetFreelancerProjects(string userId)
        {
            try
            {
                var result = await _projectService.FreelancerProjects(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Projects/All
        [HttpGet]
        [ActionName("GetAll")]
        [Route("All")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ClientWorkerModel> clientInvitation = new List<ClientWorkerModel>();
                var invitations = new List<InvitationModel>();
                string userId = GetUserId();
                //var projects = await _projectRepository.GetUserProjecList(userId);
                var projects = (List<ProjectModel>)(await _projectService.GetUserProjecList(userId)).Data;
                var currentUser = (UserInfo)(await _securityService.GetUserDetail(userId)).Data;
                if (User.IsInRole("Freelancer"))
                {

                    //invitations = await _invitationRepository.Get(includeProperties: "Client");
                    //invitations = invitations.Where(i => i.IsAccepted && i.ProjectOwner == ProjectOwner.Client && i.UserID == User.Identity.GetUserId()).ToList();
                    //invitations.ForEach(i =>
                    //{
                    //    i.Client.ClientInvitations = null;
                    //});
                    if (!currentUser.IsWorkerHasAgency)
                    {
                        clientInvitation = (List<ClientWorkerModel>)(await _clientWorkerService.GetClientInvitation(userId, WorkerType.IndividualWorker)).Data;
                    }
                    else if (currentUser.IsWorkerHasAgency)
                    {
                        clientInvitation = (List<ClientWorkerModel>)(await _clientWorkerService.GetAgencyInvitation(userId, WorkerType.AgencyWorker)).Data;
                    }
                    //List<Project> projectsInvitation = new List<Project>();
                    //projectsInvitation = clientInvitation.Select(x => x.Project).ToList();


                }
                else if (User.IsInRole("Client"))
                {
                    clientInvitation = await _projectService.GetProjectInvitation(userId, WorkerType.WorkerClient);
                    projects = projects.Where(x => x.UserId == userId).ToList();
                    //     invitations = Mapper.Map<List<Invitation>>(projectsInvitation.Where(x=>x.IsAccepted).ToList());
                    //invitations = await _invitationRepository.Get(includeProperties: "User");
                    //invitations = invitations.Where(i => i.IsAccepted && i.ProjectOwner == ProjectOwner.TeamMember && i.ClientID == User.Identity.GetUserId()).ToList();
                    //invitations.ForEach(i =>
                    //{
                    //    i.User.UserInvitations = null;
                    //});

                }
                else if (User.IsInRole("Agency"))
                {
                    clientInvitation = await _projectRepository.GetProjectInvitation(userId, WorkerType.AgencyWorker);
                    projects = projects.Where(x => x.UserId == userId).ToList();
                }
                var model = new ProjectInvitationViewModel { CurrentUser = currentUser, WorkerInvitation = clientInvitation, Projects = Mapper.Map<List<ProjectViewModel>>(projects), Invitations = Mapper.Map<List<InvitationViewModel>>(invitations) };

                var result = await _projectService.FreelancerProjects(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
    }
}
