using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Services.IService;

namespace TimeloggerCore.RestApi.Controllers
{
    public class AgencyController : BaseController
    {
        private readonly IUserService _userService;

        public AgencyController(
                IUserService userService
            )
        {
            _userService = userService;
        }


        // GET: Api/Agency/GetAllAgency
        [HttpGet]
        [Authorize]
        [ActionName("GetAllAgency")]
        [Route("Api/Agency/GetAllAgency")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAllAgency()
        {
            var result = await _userService.GetAllAgency();
            return new OkObjectResult(result);
        }


        // GET: Api/Agency/ManageInfo
        [HttpGet]
        [Authorize]
        [ActionName("ManageInfo")]
        [Route("Agency/ManageInfo")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ManageInfo()
        {
            var result = await _userService.GetAllAgency();
            return new OkObjectResult(result);
        }
    }
}
