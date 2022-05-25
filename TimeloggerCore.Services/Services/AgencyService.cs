using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;
using TimeloggerCore.Core.ISecurity;
using TimeloggerCore.Services.ICommunication;
using static TimeloggerCore.Common.Utility.Enums;
using TimeloggerCore.Common.Options;
using Microsoft.Extensions.Options;

namespace TimeloggerCore.Services
{
    public class AgencyService : BaseService<ClientAgencyModel, ClientAgency, int>, IAgencyService
    {
        private readonly IClientAgencyRepository _clientAgencyRepository;
        private readonly ISecurityService _securityService;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly ICommunicationService _communicationService;
        private readonly TimeloggerCoreOptions _timeloggerCoreOptions;

        public AgencyService(IMapper mapper, IClientAgencyRepository clientAgencyRepository, ISecurityService securityService, INotificationTemplateService notificationTemplateService, ICommunicationService communicationService, IOptionsSnapshot<TimeloggerCoreOptions> timeloggerCoreOptions, IUnitOfWork unitOfWork) : base(mapper, clientAgencyRepository, unitOfWork)
        {
            _clientAgencyRepository = clientAgencyRepository;
            _securityService = securityService;
            _notificationTemplateService = notificationTemplateService;
            _communicationService = communicationService;
            _timeloggerCoreOptions = timeloggerCoreOptions.Value;

        }
        public async Task<BaseModel> GetAgencyClients(string userId)
        {
            var result = await _clientAgencyRepository.GetAgencyClients(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientAgency>, List<ClientAgencyModel>>(result)
            };
        }

        public async Task<BaseModel> GetAgencyEmployee(string AgencyId)
        {
            var result = await _clientAgencyRepository.GetAgencyEmployee(AgencyId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ApplicationUser>, List<ApplicationUserModel>>(result)
            };
        }

        public async Task<BaseModel> GetAllWorker()
        {
            var result = await _clientAgencyRepository.GetAllWorker();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ApplicationUser>, List<ApplicationUserModel>>(result)
            };
        }

        public async Task<BaseModel> GetClientAgencies(string userId)
        {
            var result = await _clientAgencyRepository.GetClientAgencies(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientAgency>, List<ClientAgencyModel>>(result)
            };
        }

        public async Task<BaseModel> GetClientAgency(ClientAgencyModel clientAgencyModel)
        {
            var result = await _clientAgencyRepository.GetClientAgency(clientAgencyModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<ClientAgency, ClientAgencyModel>(result)
            };
        }

        public async Task<BaseModel> GetSingleClientAgencies(int Id)
        {
            var result = await _clientAgencyRepository.GetSingleClientAgencies(Id);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<ClientAgency, ClientAgencyModel>(result)
            };
        }
        public async Task<BaseModel> AddClientAgency(ClientAgencyModel clientAgencyModel)
        {
            ClientAgency clientAgency = await _clientAgencyRepository.GetClientAgency(clientAgencyModel);
            if (clientAgency == null)
            {
                var isUserExit = await _securityService.GetUserDetail(clientAgencyModel.AgencyId);
                var userInfo = (UserInfo)isUserExit.Data;
                clientAgencyModel.AgencyId = userInfo.Id;
                clientAgencyModel.IsActive = false;
                var userCreated = await Add(clientAgencyModel);
                if (userCreated.Id > 1)
                {
                    var template = await _notificationTemplateService.GetNotificationTemplate(NotificationTemplates.ConfirmClientAgency, NotificationTypes.Email);
                    var emailMessage = template.MessageBody.Replace("#Name", $"{userInfo.FirstName} { userInfo.LastName}")
                                                            .Replace("#Link", $"{_timeloggerCoreOptions.ApiUrl}?{clientAgencyModel.Id}")
                                                            .Replace("#ClientName", $"{userInfo.FirstName} ({ userInfo.LastName})");
                    var sent = await _communicationService.SendEmail(template.Subject, emailMessage, userInfo.Email);
                    //await SendEmail(clientAgency.Id, clientAgency.Agency.FullName, clientAgency.Agency.FirstName, clientAgency.Agency.LastName, clientAgency.Client.FullName, clientAgency.Client.Email, clientAgency.Agency.Email);
                    return new BaseModel
                    {
                        Success = true,
                        Data = userCreated,
                        Message = "Successfully sent invitation to agency."

                    };
                }
            }
            return new BaseModel
            {
                Success = true,
                Data = clientAgency,
                Message = "Already Exist."
            };
        }
    }
}
