using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Common.Options;
using TimeloggerCore.Core.ISecurity;
using TimeloggerCore.Services.ICommunication;
using TimeloggerCore.Services.IService;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.RestApi.Controllers
{
    [Route("api/WorkerClient")]
    [ApiController]
    public class ClientWorkerController : BaseController
    {
        private readonly IClientWorkerService _clientWorkerService;
        private readonly IProjectInvitationService _projectInvitationService;
        private readonly IPackageService _packageService;
        private readonly IProjectService _projectService;
        private readonly TimeloggerCoreOptions _timeloggerCoreOptions;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly ICommunicationService _communicationService;
        private readonly ISecurityService _securityService;
        public ClientWorkerController(
            IClientWorkerService clientWorkerService,
            IProjectInvitationService projectInvitationService,
            IOptionsSnapshot<TimeloggerCoreOptions> timeloggerCoreOptions,
            INotificationTemplateService notificationTemplateService,
            ICommunicationService communicationService,
            ISecurityService securityService,
            IPackageService packageService,
            IProjectService projectService
            )
        {
            _clientWorkerService = clientWorkerService;
            _projectInvitationService = projectInvitationService;
            _packageService = packageService;
            _projectService = projectService;
            _timeloggerCoreOptions = timeloggerCoreOptions.Value;
            _notificationTemplateService = notificationTemplateService;
            _communicationService = communicationService;
            _securityService = securityService;
        }

        // POST: Api/WorkerClient/GetAgencyEnrollWorkers
        [HttpPost]
        [Authorize]
        [ActionName("GetClientWorkers")]
        [Route("GetAgencyEnrollWorkers")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientWorkers(ClientInviteModel clientInviteModel)
        {
            try
            {
                var result = await _clientWorkerService.GetAllAgencyProjectWorker(clientInviteModel); ;
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // POST: Api/WorkerClient/DeleteAgencyEnrollWorkers
        [HttpPost]
        [Authorize]
        [ActionName("DeleteAgencyEnrollWorkers")]
        [Route("DeleteAgencyEnrollWorkers")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> DeleteAgencyEnrollWorkers(DeleteClientWorker deleteClientWorker)
        {
            try
            {
                var result = await _clientWorkerService.DeleteAgencyProjectWorker(deleteClientWorker); ;
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // POST: Api/WorkerClient/WorkerClientAdd
        [HttpPost]
        [Authorize]
        [ActionName("WorkerClientAdd")]
        [Route("WorkerClientAdd")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> WorkerClientAdd(ClientWorkersModel clientWorkerModel)
        {
            try
            {
                var userId = GetUserId();
                var result = await _projectInvitationService.ProjectAssign(userId,clientWorkerModel, InvitationType.Worker);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/WorkerClient/ResendWorkerEmail/{WorkerInvitationId}
        [HttpGet]
        [Authorize]
        [ActionName("ResendWorkerEmail")]
        [Route("ResendWorkerEmail/{WorkerInvitationId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ResendWorkerEmail(int WorkerInvitationId)
        {
            try
            {
                var result = await _clientWorkerService.GetClientWorker(WorkerInvitationId);
                var isUserExit = await _securityService.GetUserDetail(((ClientWorkerModel)(result.Data)).WorkerId);
                var userInfo = (UserInfo)isUserExit.Data;
                var template = await _notificationTemplateService.GetNotificationTemplate(NotificationTemplates.ConfirmClientAgency, NotificationTypes.Email);
                var emailMessage = template.MessageBody.Replace("#Name", $"{userInfo.FirstName} { userInfo.LastName}")
                                                        .Replace("#Link", $"{_timeloggerCoreOptions.ApiUrl}?{((ClientWorkerModel)(result.Data)).Id}")
                                                        .Replace("#ClientName", $"{userInfo.FirstName} ({ userInfo.LastName})");
                var sent = await _communicationService.SendEmail(template.Subject, emailMessage, userInfo.Email);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // POST: Api/WorkerClient/WorkerClientAdd
        [HttpPost]
        [Authorize]
        [ActionName("PutInvitation")]
        [Route("UpdateWorkerClient")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PutInvitation(ClientWorkerModel clientWorkerModel)
        {
            try
            {
                clientWorkerModel.Worker = null;
                clientWorkerModel.ProjectsInvitation = null;
                clientWorkerModel.Project = null;
                await _clientWorkerService.Update(clientWorkerModel);
                return new OkObjectResult(clientWorkerModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkerClientExists(clientWorkerModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/WorkerClient/GetClientAgencyWorker
        [HttpGet]
        [Authorize]
        [ActionName("GetClientAgencyWorker")]
        [Route("GetClientAgencyWorker")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerModel)
        {
            try
            {
                var result = await _clientWorkerService.GetClientAgencyWorker(clientAgencyWorkerModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/WorkerClient/GetClientAgencyWorkerInvitation
        [HttpGet]
        [Authorize]
        [ActionName("GetClientAgencyWorkerInvitation")]
        [Route("GetClientAgencyWorkerInvitation")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerModel)
        {
            try
            {
                var result = await _clientWorkerService.GetClientAgencyWorkerInvitation(clientAgencyWorkerModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/WorkerClient/GetClientAgencyWorkerProject
        [HttpGet]
        [Authorize]
        [ActionName("GetClientAgencyWorkerProject")]
        [Route("GetClientAgencyWorkerProject")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerModel)
        {
            try
            {
                var result = await _clientWorkerService.GetClientAgencyWorkerProject(clientAgencyWorkerModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/WorkerClient/GetClientProjects
        [HttpGet]
        [Authorize]
        [ActionName("GetClientProjects")]
        [Route("GetClientProjects")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerModel)
        {
            try
            {
                var result = await _clientWorkerService.GetClientAgencyWorkerProject(clientAgencyWorkerModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/WorkerClient/GetClientIndividualWorker
        [HttpGet]
        [Authorize]
        [ActionName("GetClientIndividualWorker")]
        [Route("GetClientIndividualWorker")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerModel)
        {
            try
            {
                var result = await _clientWorkerService.GetClientIndividualWorker(clientAgencyWorkerModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // POST: Api/WorkerClient/AddClientWorker
        [HttpPost]
        [Authorize]
        [ActionName("AddClientWorker")]
        [Route("AddClientWorker")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddClientWorker(ClientWorkersModel clientWorkerModel)
        {
            try
            {
                var checkUserPackage = await UserPackageCheck(clientWorkerModel);
                var userId = GetUserId();
                if (checkUserPackage)
                {
                    var projectAssign = await _projectInvitationService.ProjectAssign(userId, clientWorkerModel, InvitationType.Client);
                    return new OkObjectResult(projectAssign);
                }
                return new OkObjectResult(clientWorkerModel);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // POST: Api/WorkerClient/AddWorkerClient
        [HttpPost]
        [Authorize]
        [ActionName("AddWorkerClient")]
        [Route("AddWorkerClient")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddWorkerClient(ClientWorkersModel clientWorkerModel)
        {
            try
            {
                var userId = GetUserId();
                    var projectAssign = await _projectInvitationService.ProjectAssign(userId, clientWorkerModel, InvitationType.WorkerToClient);
                    return new OkObjectResult(projectAssign);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        public async Task<bool> UserPackageCheck(ClientWorkersModel clientWorkerModel)
        {
            List<ClientWorkerModel> projectInviation = new List<ClientWorkerModel>();
            List<string> workerIds = new List<string>();
            string[] Ids;
            bool isUserPackageExceed = true;
            projectInviation = (List<ClientWorkerModel>)((await _clientWorkerService.GetProjectInvitation(clientWorkerModel.AgencyId, WorkerType.WorkerClient)).Data);
            workerIds.AddRange(projectInviation.Select(x => x.ProjectsInvitation.AgencyId).ToList());
            var clientIndividualInvitation = (List<ClientWorkerModel>)((await _clientWorkerService.GetUserProjecInviationtList(clientWorkerModel.AgencyId, WorkerType.IndividualWorker)).Data);
            if (clientIndividualInvitation.Count() > 0)
            {
                projectInviation.AddRange(clientIndividualInvitation);
                workerIds.AddRange(clientIndividualInvitation.Select(x => x.WorkerId).ToList());
            }
            Ids = workerIds.Distinct().ToArray();
            //List<ClientWorker> clientWorker1 = await _clientWorkerRepository.GetAllClientWorker(clientWorkerViewModel.AgencyId);
            var clientWorker = (List<ClientWorkerModel>)((await _clientWorkerService.GetAllClientWorker(clientWorkerModel.AgencyId)).Data);

            var clientWorkerCount = clientWorker.Count;
            var packagelist = (PackageModel)(await _packageService.GetAllPackage(clientWorkerModel.AgencyId)).Data;
            if (Ids.Length > 0)
            {
                if (Ids.Length >= packagelist.MemberAllowed)
                {
                    isUserPackageExceed = false;
                }
            }
            return isUserPackageExceed;
            
        }
        private async Task<bool> WorkerClientExists(int id)
        {
            return (await _clientWorkerService.Get()).Count(e => e.Id == id) > 0;
        }
    }
}
