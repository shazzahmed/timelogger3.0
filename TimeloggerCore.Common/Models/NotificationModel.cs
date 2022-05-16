﻿using TimeloggerCore.Common.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public NotificationTypes NotificationTypeId { get; set; }
        public string Recipient { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }
        public int StatusId { get; set; }
        public int Attempts { get; set; }
        public DateTime ProcessedAt { get; set; }
        public string Result { get; set; }
    }
}
