using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Data.Repository
{
    public class NotificationTemplateRepository : BaseRepository<NotificationTemplate, int>, INotificationTemplateRepository
    {
        public NotificationTemplateRepository(ISqlServerDbContext context) : base(context)
        {
        }
    }
}
