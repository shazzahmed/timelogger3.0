using TimeloggerCore.Common.Utility.Constants;
using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Services.IService
{
    public interface INotificationTemplateService : IBaseService<NotificationTemplateModel, NotificationTemplate, int>
    {
        Task<NotificationTemplateModel> GetNotificationTemplate(NotificationTemplates notificationTemplates, NotificationTypes notificationTypes);
    }
}
