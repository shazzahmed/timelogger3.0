using Microsoft.Ajax.Utilities;
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
    public class InvitationsController : BaseController
    {
        private readonly IInvitationService _invitationService;
        private readonly ITimeLogService _timeLogService;
        private readonly IPaymentService _paymentService;
        private readonly IPackageService _packageService;
        private readonly IProjectService _projectService;
        private readonly IClientWorkerService _clientWorkerService;
        private readonly IAgencyService _agencyService;
        private readonly ISecurityService _securityService;

        public InvitationsController(
                ITimeLogService timeLogService,
                IProjectService projectService,
                IInvitationService invitationService,
                IPaymentService paymentService,
                IClientWorkerService clientWorkerService,
                ISecurityService securityService,
                IPackageService packageService, 
                IAgencyService agencyService)
        {
            _timeLogService = timeLogService;
            _projectService = projectService;
            _invitationService = invitationService;
            _paymentService = paymentService;
            _clientWorkerService = clientWorkerService;
            _securityService = securityService;
            _packageService = packageService;
            _agencyService = agencyService;
        }


        // POST: Api/Invitations/GetTeamleadMember/{userId}
        [HttpPost]
        [ActionName("GetTeamleadMember")]
        [Route("GetTeamleadMember/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetTeamleadMember(string userId)
        {
            try
            {
                PaymentsModel paymentViewModel = new PaymentsModel
                {
                    //Invitations = await _invitationRepository.GetInvitationsList(userId),
                    Packages = (PackageModel)(await _packageService.GetAllPackage(userId)).Data,
                    Payments = (List<PaymentModel>)(await _paymentService.GetUserInvoice(userId)).Data,
                    EmployeeWorkingHour = (List<TimeLogModel>)(await _timeLogService.GetTeamWorkerTime(userId)).Data
                };
                return new OkObjectResult(paymentViewModel);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/GetProjectWorkerInvitations/{projectId}
        [HttpGet]
        [ActionName("GetProjectWorkerInvitations")]
        [Route("GetProjectWorkerInvitations/{projectId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetProjectInvitations(int ProjectId)
        {
            try
            {
                InvitationsModel workerClientViewModel = new InvitationsModel();
                string userId = GetUserId();
                workerClientViewModel.ClientWorkers = (List<ClientWorkerModel>)(await _clientWorkerService.GetWorkerInvitation(userId, ProjectId, InvitationType.Worker)).Data;
                var project = (ProjectModel)(await _projectService.GetUserProjects((ProjectId).ToString())).Data;
                workerClientViewModel.Invitation = new InvitationModel();
                workerClientViewModel.Invitation.Project = new ProjectModel();
                workerClientViewModel.Invitation.Project.Id = project.Id;
                workerClientViewModel.Invitation.Project.Title = project.Title;
                workerClientViewModel.Invitation.Project.Description = project.Description;
                workerClientViewModel.Invitation.Project.Color = project.Color;
                workerClientViewModel.Invitation.Project.ApplicationUser = project.ApplicationUser;
                return new OkObjectResult(workerClientViewModel);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/WorkerClientSearchByEmail
        [HttpGet]
        [ActionName("WorkerClientSearchByEmail")]
        [Route("WorkerClientSearchByEmail/{projectId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> WorkerClientSearchByEmail(string Email)
        {
            try
            {
                var result = await _securityService.GetUser(Email);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/GetInvitations
        [HttpGet]
        [ActionName("GetInvitations")]
        [Route("GetInvitations")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetInvitations()
        {
            try
            {
                var result = await _invitationService.Get(null, null,i => i.Project, i => i.User, i => i.Client);
                foreach (var invitation in result)
                {
                    invitation.Project.Invitations = null;
                    if (invitation.Client != null)
                    {
                        invitation.Client.ClientInvitations = null;
                    }

                    if (invitation.User != null)
                    {
                        invitation.User.UserInvitations = null;
                    }

                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/ActiveProjects
        [HttpGet]
        [ActionName("ActiveProjects")]
        [Route("ActiveProjects")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ActiveProjects()
        {
            try
            {
                var invitations = (IEnumerable<InvitationModel>)(await _invitationService.GetActiveProjects(GetUserId())).Data;
                
                var logs = (List<TimeLogModel>)(await _timeLogService.GetActiveProjects(invitations.Select(x => x.UserID).ToList())).Data;
                var activeLogs = invitations;
                activeLogs.ForEach(l => l.IsActive = logs.Select(i => i.UserId).Contains(l.UserID) && logs.Select(i => i.ProjectId).Contains(l.ProjectID));
                return new OkObjectResult(activeLogs);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/ActiveClientProjects
        [HttpGet]
        [ActionName("ActiveClientProjects")]
        [Route("ActiveClientProjects")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ActiveClientProjects()
        {
            try
            {
                var invitations = (List<ClientWorkerModel>)(await _clientWorkerService.GetClientActiveProjects(GetUserId())).Data;
                var clientInvitation = (List<ClientWorkerModel>)(await _clientWorkerService.GetProjectInvitation(GetUserId(), WorkerType.WorkerClient)).Data;
                if (clientInvitation != null)
                {
                    invitations.AddRange(clientInvitation);
                }

                var logs = (List<TimeLogModel>)(await _timeLogService.GetActiveProjects(invitations.Select(x => x.WorkerId).ToList())).Data;

                var activeLogs = invitations;
                activeLogs.ForEach(l => l.IsActive = logs.Select(i => i.UserId).Contains(l.WorkerId) && logs.Select(i => i.ProjectId).Contains(l.ProjectId));
                return new OkObjectResult(activeLogs);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/TeamMembers/{userId}
        [HttpGet]
        [ActionName("GetTeamMembers")]
        [Route("TeamMembers/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetTeamMembers(string userId)
        {
            try
            {
                var clientAgency = (List<ClientAgencyModel>)(await _agencyService.GetClientAgencies(userId)).Data;
                clientAgency = clientAgency.Where(x => x.IsAgencyAccepted).ToList();
                return new OkObjectResult(clientAgency);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/GetWorkerClient/{Id}
        [HttpGet]
        [ActionName("GetWorkerClient")]
        [Route("GetWorkerClient/{Id}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetWorkerClient(int id)
        {
            try
            {
                var clientWorker = (ClientWorkerModel)(await _clientWorkerService.GetWorkerClientById(id)).Data;
                return new OkObjectResult(clientWorker);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/GetInvitation/{Id}
        [HttpGet]
        [ActionName("GetInvitation")]
        [Route("GetInvitation/{Id}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetInvitation(int id)
        {
            try
            {
                var result = await _invitationService.FirstOrDefaultAsync(x=> x.ID == id,null, i=> i.Project, i => i.User, i => i.Client);
                
                if (result == null)
                {
                    return NotFound();
                }
                result.Project.Invitations = null;
                if (result.Client != null)
                {
                    result.Client.ClientInvitations = null;
                }

                if (result.User != null)
                {
                    result.User.UserInvitations = null;
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // POST: Api/Invitations/PostInvitation
        [HttpPost]
        [ActionName("PostInvitation")]
        [Route("PostInvitation")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PostInvitation(InvitationModel invitationModel)
        {
            try
            {
                var result = await _invitationService.Add(invitationModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Invitations/DeleteInvitation
        [HttpDelete]
        [ActionName("DeleteInvitation")]
        [Route("DeleteInvitation")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> DeleteInvitation(InvitationModel invitationModel)
        {
            try
            {
                if (await _invitationService.FirstOrDefaultAsync(x=> x.ID == invitationModel.ID) != null)
                {
                    await _invitationService.SoftDelete(invitationModel);
                    return new OkObjectResult("Succesful");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
    }
}
