using TimeloggerCore.Common.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class NotificationTypeModel
    {
        public NotificationTypes Id { get; set; }
        public string Name { get; set; }
    }
}
