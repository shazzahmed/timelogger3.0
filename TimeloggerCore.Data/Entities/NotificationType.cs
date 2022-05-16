using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Entities
{
    public class NotificationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public NotificationTypes Id { get; set; }
        public string Name { get; set; }
    }
}
