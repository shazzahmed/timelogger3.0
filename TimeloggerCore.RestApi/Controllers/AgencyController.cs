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
        [Authorize]
        [ActionName("GetAllAgency")]
        [Route("GetAllAgency")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAllAgency()
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


        // GET: Api/Agency/ManageInfo
        [HttpGet]
        [Authorize]
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
