using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Extensions;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Services.IService;

namespace TimeloggerCore.RestApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
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
        [AuthorizeCustom]
        [ActionName("GetAllAgency")]
        [Route("GetAllAgency")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAllAgency()
        {
            if (User.TryGetExternalId(out string externalId))
            {
                //isAnonymous = AuthenticationHelper.IsAnonymous(externalId);
            }
            try
            {
                var result = await _userService.GetAllAgency();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }


        // GET: Api/Agency/ManageInfo
        [HttpGet]
        [ActionName("ManageInfo")]
        [Route("ManageInfo")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ManageInfo()
        {
            try
            {
                var result = await _userService.GetAllAgency();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
    }
}
