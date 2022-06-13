using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using TimeloggerCore.Common.Helpers;
using TimeloggerCore.Common.Utility.Constants;
using IPAddress = TimeloggerCore.Common.Utility.Constants.IPAddress;

namespace TimeloggerCore.Common.Filters
{
    public class AuthorizeCustom : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            var userip = NetworkHelper.GetRemoteUserIp(context.HttpContext.Request.Headers["x-forwarded-for"]);
            if (!IPAddress.IPAddresses.Contains(userip))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new UnauthorizedObjectResult("Unauthorized !");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
