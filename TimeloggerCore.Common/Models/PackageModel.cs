using System;
using System.Collections.Generic;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class PackageModel
    {
        public int Id { get; set; }
        public PackageType PackageTypeId { get; set; }
        public int MemberAllowed { get; set; }
        public string UserId { get; set; }
        public ApplicationUserModel User { get; set; }
    }
}
