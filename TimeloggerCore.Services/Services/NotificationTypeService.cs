using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services
{
    public class NotificationTypeService : BaseService<NotificationTypeModel, NotificationType, int>, INotificationTypeService
    {
        private readonly INotificationTypeRepository notificationTypeRepository;

        public NotificationTypeService(IMapper mapper, INotificationTypeRepository notificationTypeRepository, IUnitOfWork unitOfWork) : base(mapper, notificationTypeRepository, unitOfWork)
        {
            this.notificationTypeRepository = notificationTypeRepository;
        }
    }
}
