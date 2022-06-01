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
using TimeloggerCore.Core.ISecurity;
using TimeloggerCore.Services.ICommunication;
using TimeloggerCore.Common.Options;
using Microsoft.Extensions.Options;
using System.Linq;

namespace TimeloggerCore.Services
{
    public class ProjectInvitationService : BaseService<ProjectsInvitationModel, ProjectsInvitation, int>, IProjectInvitationService
    {
        private readonly IProjectInvitationRepository _projectInvitationRepository;
        private readonly IClientWorkerService _clientWorkerService;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly ICommunicationService _communicationService;
        private readonly ISecurityService _securityService;
        private readonly TimeloggerCoreOptions _timeloggerCoreOptions;

        public ProjectInvitationService(
            IMapper mapper, 
            IProjectInvitationRepository projectInvitationRepository, 
            IClientWorkerService clientWorkerService, 
            INotificationTemplateService notificationTemplateService, 
            ICommunicationService communicationService,
            IOptionsSnapshot<TimeloggerCoreOptions> timeloggerCoreOptions,
            ISecurityService securityService, 
            IUnitOfWork unitOfWork
            ) : base(mapper, projectInvitationRepository, unitOfWork)
        {
            _projectInvitationRepository = projectInvitationRepository;
            _clientWorkerService = clientWorkerService;
            _notificationTemplateService = notificationTemplateService;
            _communicationService = communicationService;
            _timeloggerCoreOptions = timeloggerCoreOptions.Value;
            _securityService = securityService;
        }
        public async Task<BaseModel> ProjectAssign(string userId, ClientWorkersModel clientWorkerModel, InvitationType invitationType)
        {
            var isUserExit = await _securityService.GetUserDetail(clientWorkerModel.AgencyId);
            var userInfo = (UserInfo)isUserExit.Data;
            var clientWorkerList = (List<ClientWorkerModel>)(await _clientWorkerService.GetWorkerInvitation(userId, Convert.ToInt32(clientWorkerModel.ProjectId), InvitationType.Worker)).Data;
            var result = await _projectInvitationRepository.ProjectAssign(clientWorkerModel.ProjectId, clientWorkerModel.UserId, invitationType);
            int projectId = 0;
            if (isUserExit == null)
            {
                ClientWorkerModel clientWorker = new ClientWorkerModel();
                ProjectsInvitationModel projectsInvitationModel = null;
                if (result == null)
                {
                    projectsInvitationModel = new ProjectsInvitationModel
                    {
                        CreatedOn = DateTime.Now,
                        AgencyId = invitationType == InvitationType.Worker ? clientWorkerModel.UserId : clientWorkerModel.AgencyId,
                        ProjectId = Convert.ToInt32(clientWorkerModel.ProjectId),
                        IsActive = true,
                        IsAccepted = true,
                        ExistingUser = true,
                        EmailAddress = "",
                        InvitationType = invitationType == InvitationType.Worker ? InvitationType.Worker : InvitationType.Client
                    };
                    projectsInvitationModel = await Add(projectsInvitationModel);
                    projectId = projectsInvitationModel.Id;
                }
                else
                {
                    projectId = result.Id;
                    projectsInvitationModel = mapper.Map<ProjectsInvitation, ProjectsInvitationModel>(result);
                }
                var alreadySendInviationToWorker = (await _clientWorkerService.AlreadySendInvitation(clientWorkerModel.WorkerIds.FirstOrDefault(), projectsInvitationModel.Id, projectsInvitationModel.ProjectId, clientWorkerModel.AgencyId)).Data;
                if ((invitationType == InvitationType.Client && alreadySendInviationToWorker == null) || invitationType == InvitationType.Worker)
                {

                    clientWorker.WorkerType = clientWorkerModel.WorkerType;
                    clientWorker.IsActive = true;
                    clientWorker.ProjectId = Convert.ToInt32(clientWorkerModel.ProjectId);
                    clientWorker.ProjectsInvitationId = projectId;
                    clientWorker.WorkerId = clientWorkerModel.WorkerIds.FirstOrDefault();
                    // clientWorker.WorkerId = item;
                    clientWorker.ExistingUser = invitationType == InvitationType.Client ? true : false;
                    clientWorker.EmailAddress = clientWorkerModel.AgencyId;
                    clientWorker.IsAccepted = false;
                    clientWorker.CanAddManualTime = false;
                    clientWorker.CanEditTimeLog = false;
                    clientWorker.MaximumHours = 0;
                    clientWorker.MinimumHours = 0;
                    clientWorker.Status = MemberStatus.Active;
                    clientWorker.RatePerHour = 0;
                    clientWorker.CreatedOn = DateTime.Now.ToLocalTime();
                    clientWorker.WorkerType = clientWorkerModel.WorkerType;
                    clientWorker =await _clientWorkerService.Add(clientWorker);
                    isUserExit = await _securityService.GetUserDetail(clientWorker.WorkerId);
                    var worker = (UserInfo)isUserExit.Data;
                    var template = await _notificationTemplateService.GetNotificationTemplate(NotificationTemplates.ConfirmClientAgency, NotificationTypes.Email);
                    var emailMessage = template.MessageBody.Replace("#Name", $"{userInfo.FirstName} { userInfo.LastName}")
                                                            .Replace("#Link", $"{_timeloggerCoreOptions.ApiUrl}?{clientWorker.Id}")
                                                            .Replace("#ClientName", $"{userInfo.FirstName} ({ userInfo.LastName})");
                    var sent = await _communicationService.SendEmail(template.Subject, emailMessage, userInfo.Email);
                    
                    
                    if (invitationType == InvitationType.Worker)
                    {
                        return new BaseModel
                        {
                            Success = true,
                            Data = new AddWorkerClientReponse
                            {
                                ClientWorker = clientWorker,
                                WorkerResponse = AddWorkerClientStatus.UserNotExit,
                                ClientWorkerList = clientWorkerList
                            },
                            Message = "Invitation has been sent successfully."
                        };
                    }
                    if (invitationType == InvitationType.Client)
                    {
                        return new BaseModel
                        {
                            Success = true,
                            Data = new AddWorkerClientReponse
                            {
                                ClientWorker = clientWorker,
                                WorkerResponse = AddWorkerClientStatus.UserNotExit,
                                ClientWorkerList = clientWorkerList
                            },
                            Message = "Invitation has been sent successfully."
                        };
                    }
                }
                else if (invitationType == InvitationType.WorkerToClient)
                {
                    var clientWorkerModelList = new List<ClientWorkerModel>();
                    foreach (var item in clientWorkerModel.WorkerIds)
                    {
                        clientWorker.WorkerType = clientWorkerModel.WorkerType;
                        clientWorker.IsActive = true;
                        clientWorker.ProjectId = Convert.ToInt32(clientWorkerModel.ProjectId);
                        clientWorker.ProjectsInvitationId = projectId;
                        clientWorker.WorkerId = item;
                        clientWorker.IsAccepted = true;
                        clientWorker.CanAddManualTime = false;
                        clientWorker.CanEditTimeLog = false;
                        clientWorker.MaximumHours = 0;
                        clientWorker.MinimumHours = 0;
                        clientWorker.Status = MemberStatus.Active;
                        clientWorker.RatePerHour = 0;
                        clientWorker.CreatedOn = DateTime.Now.ToLocalTime();
                        clientWorker.WorkerType = clientWorkerModel.WorkerType;
                        clientWorkerModelList.Add(clientWorker);
                    }
                       await _clientWorkerService.AddRange(clientWorkerModelList);
                }
                //await SendEmail(clientWorker.Id, clientWorker.Worker.FullName, clientWorker.Worker.FirstName, clientWorker.Worker.LastName, projectsInvitation.Agency.FullName, clientWorker.Worker.Email, projectsInvitation.Agency.Email);
                return new BaseModel
                {
                    Success = true,
                    Data = new AddWorkerClientReponse
                    {
                        ClientWorker = clientWorker,
                        WorkerResponse = AddWorkerClientStatus.UserNotExit,
                        ClientWorkerList = clientWorkerList
                    },
                    Message = "Invitation has been sent successfully."
                };
            }
            else
            {
                ClientWorkerModel alreadySendInvitation = null;
                if (result != null)
                    alreadySendInvitation = (ClientWorkerModel)(await _clientWorkerService.AlreadyInvitationExit(Convert.ToInt32(clientWorkerModel.ProjectId), result.Id, userInfo.Id, clientWorkerModel.WorkerType)).Data;
                if (alreadySendInvitation == null)
                {
                    ProjectsInvitationModel projectsInvitation = null;
                    if (result == null)
                    {
                        projectsInvitation = new ProjectsInvitationModel
                        {
                            CreatedOn = DateTime.Now,
                            AgencyId = clientWorkerModel.UserId,
                            ProjectId = Convert.ToInt32(clientWorkerModel.ProjectId),
                            IsActive = true,
                            IsAccepted = true,
                            ExistingUser = true,
                            EmailAddress = "",
                            InvitationType = InvitationType.Worker
                        };
                        projectsInvitation = await Add(projectsInvitation);
                        projectId = projectsInvitation.Id;
                    }
                    else
                    {
                        projectsInvitation = mapper.Map<ProjectsInvitation, ProjectsInvitationModel>(result);
                        projectId = result.Id;
                    }
                    ClientWorkerModel clientWorker = new ClientWorkerModel();

                    clientWorker.WorkerType = clientWorkerModel.WorkerType;
                    clientWorker.IsActive = true;
                    clientWorker.ProjectId = Convert.ToInt32(clientWorkerModel.ProjectId);
                    clientWorker.ProjectsInvitationId = projectId;
                    clientWorker.WorkerId = userInfo.Id;
                    clientWorker.ExistingUser = true;
                    clientWorker.IsAccepted = false;
                    clientWorker.CanAddManualTime = false;
                    clientWorker.CanEditTimeLog = false;
                    clientWorker.MaximumHours = 0;
                    clientWorker.MinimumHours = 0;
                    clientWorker.Status = MemberStatus.Active;
                    clientWorker.RatePerHour = 0;
                    clientWorker.CreatedOn = DateTime.Now.ToLocalTime();
                    clientWorker.WorkerType = clientWorkerModel.WorkerType;
                    clientWorker = await _clientWorkerService.Add(clientWorker);
                    var template = await _notificationTemplateService.GetNotificationTemplate(NotificationTemplates.ConfirmClientAgency, NotificationTypes.Email);
                    var emailMessage = template.MessageBody.Replace("#Name", $"{userInfo.FirstName} { userInfo.LastName}")
                                                            .Replace("#Link", $"{_timeloggerCoreOptions.ApiUrl}?{clientWorker.Id}")
                                                            .Replace("#ClientName", $"{userInfo.FirstName} ({ userInfo.LastName})");
                    var sent = await _communicationService.SendEmail(template.Subject, emailMessage, userInfo.Email);
                    return new BaseModel
                    {
                        Success = true,
                        Data = new AddWorkerClientReponse
                        {
                            ClientWorker = clientWorker,
                            WorkerResponse = AddWorkerClientStatus.Success,
                            ClientWorkerList = clientWorkerList
                        },
                        Message = "Invitation has been sent successfully."
                    };
                    //await SendEmail(clientWorker.Id, clientWorker.Worker.FullName, clientWorker.Worker.FirstName, clientWorker.Worker.LastName, projectsInvitation.Agency.FullName, clientWorker.Worker.Email, projectsInvitation.Agency.Email);

                }
                else
                {
                    return new BaseModel
                    {
                        Success = true,
                        Data = new AddWorkerClientReponse
                        {
                            WorkerResponse = AddWorkerClientStatus.AlreadySentInvitation,
                            Project = new ProjectModel(),
                            ClientWorkerList = clientWorkerList
                        },
                        Message = $"Already Send Invitation to this client({userInfo.Email})."
                    };
                }
            }
        }
    }
}
