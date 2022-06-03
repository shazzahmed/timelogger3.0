using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Services.IService;

namespace TimeloggerCore.RestApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class MeetingController : BaseController
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(
            IMeetingService meetingService
            )
        {
            _meetingService = meetingService;
        }


        // GET: Api/Meeting/All
        [HttpGet]
        [ActionName("MeetingList")]
        [Route("All")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> MeetingList()
        {
            try
            {
                var result = await _meetingService.GetAllMeeting(GetUserId());
                if (User.IsInRole("Freelancer"))
                {
                }
                else if (User.IsInRole("Client"))
                {
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }


        // GET: Api/Meeting/MeetingPost
        [HttpGet]
        [ActionName("MeetingPost")]
        [Route("MeetingPost")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> MeetingPost(MeetingModel meetingModel)
        {
            try
            {
                var result = await _meetingService.Add(meetingModel);
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
