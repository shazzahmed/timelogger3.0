using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class MessageModel
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public MessageType MessageType { get; set; }
        public string Link { get; set; }
        public string LinkText { get; set; }
    }
}
