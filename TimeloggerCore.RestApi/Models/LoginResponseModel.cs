using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.RestApi.Models
{
    public class LoginResponseModel
    {
        public LoginStatus Status { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
