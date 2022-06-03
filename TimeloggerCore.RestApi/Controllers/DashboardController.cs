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
    public class DashboardController : BaseController
    {
        private readonly ITimeLogService _timeLogService;
        private readonly IProjectService _projectService;
        private readonly IInvitationService _invitationService;
        private readonly IPaymentService _paymentService;
        private readonly IClientWorkerService _clientWorkerService;
        private readonly IWorkSessionService _workSessionService;
        private readonly ISecurityService _securityService;

        public DashboardController(
                ITimeLogService timeLogService,
                IProjectService projectService,
                IInvitationService invitationService,
                IPaymentService paymentService,
                IClientWorkerService clientWorkerService,
                IWorkSessionService workSessionService, 
                ISecurityService securityService)
        {
            _timeLogService = timeLogService;
            _projectService = projectService;
            _invitationService = invitationService;
            _paymentService = paymentService;
            _clientWorkerService = clientWorkerService;
            _workSessionService = workSessionService;
            _securityService = securityService;
        }


        // POST: Api/Dashboard/GetData
        [HttpPost]
        [ActionName("GetData")]
        [Route("Data")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetData(DashboardDataModel dashboardDataViewModel)
        {
            try
            {
                List<ClientWorkerModel> projectInviation = new List<ClientWorkerModel>();
                List<string> workerIds = new List<string>();
                string[] Ids;
                DashboardBaseModel dashboardBaseViewModel = new DashboardBaseModel();
                dashboardBaseViewModel.Users = new List<ApplicationUserModel>();
                var userProject = (List<ProjectModel>)(await _projectService.GetUserProjecList(dashboardDataViewModel.UserId)).Data;
                if (User.IsInRole("Freelancer"))
                {
                    dashboardBaseViewModel.ClientWorkerInvitation = (List<ClientWorkerModel>)(await _clientWorkerService.GetAllInvitation(dashboardDataViewModel.UserId)).Data;
                    var currentUserInfo = (UserInfo)(await _securityService.GetUserDetail(dashboardDataViewModel.UserId)).Data;
                    projectInviation = (List<ClientWorkerModel>)(await _clientWorkerService.GetProjectInvitation(dashboardDataViewModel.UserId, currentUserInfo.IsWorkerHasAgency ? WorkerType.AgencyWorker : WorkerType.IndividualWorker)).Data;
                    
                    dashboardBaseViewModel.ClientWorkerInvitation.AddRange(projectInviation);
                    //workerIds = projectInviation.Select(x => x.WorkerId).ToList();
                    workerIds.Add(dashboardDataViewModel.UserId);
                    dashboardBaseViewModel.Users.AddRange(dashboardBaseViewModel.ClientWorkerInvitation.Select(x => x.Worker).ToList());

                }
                else if (User.IsInRole("Client"))
                {
                    projectInviation  = (List<ClientWorkerModel>)(await _clientWorkerService.GetProjectInvitation(dashboardDataViewModel.UserId, WorkerType.WorkerClient)).Data;
                    dashboardBaseViewModel.Users.AddRange(projectInviation.Select(x => x.ProjectsInvitation.Agency).ToList());
                    workerIds.AddRange(projectInviation.Select(x => x.ProjectsInvitation.AgencyId).ToList());
                    var clientIndividualInvitation = (List<ClientWorkerModel>)(await _clientWorkerService.GetUserProjecInviationtList(dashboardDataViewModel.UserId, WorkerType.IndividualWorker)).Data;
                    if (clientIndividualInvitation.Count() > 0)
                    {
                        projectInviation.AddRange(clientIndividualInvitation);
                        dashboardBaseViewModel.Users.AddRange(clientIndividualInvitation.Select(x => x.Worker).ToList());
                        workerIds.AddRange(clientIndividualInvitation.Select(x => x.WorkerId).ToList());
                    }
                    dashboardBaseViewModel.ClientWorkerInvitation = projectInviation;
                }
                if (projectInviation.Count() > 0)
                {
                    userProject.AddRange(projectInviation.Distinct().Where(x => x.IsAccepted && !x.IsDeleted).Select(x => x.Project).ToList());
                }

                Ids = workerIds.Distinct().ToArray();
                var TimeLogs = (List<TimeLogModel>)(await _timeLogService.GetAllWorkerProjectTimeLogs(Ids, dashboardDataViewModel.Type)).Data;
                userProject = userProject.DistinctBy(x => x.Id).ToList();
                dashboardBaseViewModel.Users = dashboardBaseViewModel.Users.DistinctBy(x => x.Id).ToList();
                dashboardBaseViewModel.Projects = userProject;
                if (dashboardDataViewModel.IsWorkSessionRequired)
                {
                    int[] timeLogsId = dashboardBaseViewModel.TimeLogs.Select(x => x.Id).ToArray();
                    // return Mapper.Map<List<WorkSessionViewModel>>(await _workSessionRepository.Get(includeProperties: "TimeLog,TimeLog.Project"));
                    dashboardBaseViewModel.WorkSessionViewModels = new List<WorkSessionModel>();
                    dashboardBaseViewModel.WorkSessionViewModels = (List<WorkSessionModel>)(await _workSessionService.WorkSessionsList(timeLogsId)).Data;
                }
                return new OkObjectResult(userProject);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Dashboard/GetUserProject/{userId}
        [HttpGet]
        [ActionName("GetUserProject")]
        [Route("GetUserProject/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetUserProject(string userId)
        {
            try
            {
                List<ProjectModel> projects = (List<ProjectModel>)(await _projectService.GetUserProjecList(userId)).Data;
                projects = projects.Where(x => x.UserId == userId).ToList();
                WorkerAgencyModel workerAgencyViewModel = new WorkerAgencyModel();
                UserInfo worker = new UserInfo();
                worker = (UserInfo)(await _securityService.GetUserDetail(userId)).Data;
                workerAgencyViewModel.IsAgencyAccepted = worker.IsAgencyApproved;
                workerAgencyViewModel.IsWorkerHasAgency = worker.IsWorkerHasAgency;
                if (worker.IsWorkerHasAgency)
                    workerAgencyViewModel.WorkerAgency = (UserInfo)(await _securityService.GetUserDetail(userId)).Data;

                var paymentStatus = (PaymentModel)(await _paymentService.CurrentActivePayment(userId)).Data;

                workerAgencyViewModel.IsPaymentExpire = false;
                if (paymentStatus != null)
                {
                    DateTime currentDate = DateTime.Now;
                    if (!paymentStatus.IsPaid)
                    {
                        int days = DateTime.Compare(paymentStatus.PaymentDueDate.Value.Date, currentDate.Date);
                        if (days == 0 || days < 0)
                        {
                            workerAgencyViewModel.IsPaymentExpire = true;
                        }
                    }
                }
                if (projects?.Count() > 0)
                    workerAgencyViewModel.IsProjectExit = true;
                else
                    workerAgencyViewModel.IsProjectExit = false;
                return new OkObjectResult(workerAgencyViewModel);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
    }
}
