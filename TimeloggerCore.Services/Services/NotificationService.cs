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
    public class NotificationService : BaseService<NotificationModel, Notification, int>, INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(
            IMapper mapper, 
            INotificationRepository notificationRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, notificationRepository, unitOfWork)
        {
            this.notificationRepository = notificationRepository;
        }
    }
}
