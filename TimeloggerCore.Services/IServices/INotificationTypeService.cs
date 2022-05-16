using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services.IService
{
    public interface INotificationTypeService : IBaseService<NotificationTypeModel, NotificationType, int>
    {
    }
}
