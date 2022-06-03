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
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(
                IFeedbackService feedbackService
            )
        {
            _feedbackService = feedbackService;
        }


        // POST: Api/Feedback/PostFeedback
        [HttpPost]
        [ActionName("PostFeedback")]
        [Route("PostFeedback")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PostFeedback(FeedbackModel feedback)
        {
            try
            {
                var result = await _feedbackService.Add(feedback);
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
