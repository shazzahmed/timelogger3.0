using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeloggerCore.RestApi.Controllers
{
    [Authorize]
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
    }
}
