using AutoMapper;
using TimeloggerCore.Common.Utility.Constants;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Services
{
    public class NotificationTemplateService : BaseService<NotificationTemplateModel, NotificationTemplate, int>, INotificationTemplateService
    {
        private readonly INotificationTemplateRepository _notificationTemplateRepository;

        public NotificationTemplateService(IMapper mapper, INotificationTemplateRepository notificationTemplateRepository, IUnitOfWork unitOfWork) : base(mapper, notificationTemplateRepository, unitOfWork)
        {
            _notificationTemplateRepository = notificationTemplateRepository;
        }

        public async Task<NotificationTemplateModel> GetNotificationTemplate(NotificationTemplates notificationTemplates, NotificationTypes notificationTypes)
        {
            var template = await _notificationTemplateRepository.FirstOrDefaultAsync(x => x.Id == notificationTemplates && x.NotificationTypeId == notificationTypes);
            return mapper.Map<NotificationTemplate, NotificationTemplateModel>(template); ;
        }
    }
}
