using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Core.ISecurity;
using TimeloggerCore.Services.IService;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.RestApi.Controllers
{
    [Authorize]
    [Route("Api/UserInvitation")]
    [ApiController]
    public class InvitationRequestController : BaseController
    {
        private readonly IInvitationRequestService _invitationRequestService;
        private readonly IPaymentService _paymentService;
        private readonly IAgencyService _agencyService;
        private readonly ISecurityService _securityService;

        public InvitationRequestController(
            IInvitationRequestService invitationRequestService,
            IPaymentService paymentService,
            IAgencyService agencyService, 
            ISecurityService securityService)
        {
            _invitationRequestService = invitationRequestService;
            _paymentService = paymentService;
            _agencyService = agencyService;
            _securityService = securityService;
        }


        // POST: Api/UserInvitation/AddInvitation
        [HttpPost]
        [ActionName("AddInvitation")]
        [Route("AddInvitation")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddInvitation(InvitationRequestModel invitationRequestModel)
        {
            string message = "Invalid request.";
            try
            {
                var invitationSentTo = (IList<string>)(await _securityService.GetUserRoles(invitationRequestModel.ToUserId)).Data;
                var invitationSentFrom = (IList<string>)(await _securityService.GetUserRoles(invitationRequestModel.FromUserId)).Data;
                if (invitationSentTo.Contains("Agency") && invitationSentFrom.Contains("Client"))
                {
                    invitationRequestModel.InvitationType = InvitationType.ClientToAgency;
                }
                else if (invitationSentTo.Contains("Client") && invitationSentFrom.Contains("Agency"))
                {
                    invitationRequestModel.InvitationType = InvitationType.AgencyToClient;
                }
                else if (invitationSentTo.Contains("Client") && invitationSentFrom.Contains("Freelancer"))
                {
                    invitationRequestModel.InvitationType = InvitationType.WorkerToClient;
                }
                else if (invitationSentTo.Contains("Freelancer") && invitationSentFrom.Contains("Client"))
                {
                    invitationRequestModel.InvitationType = InvitationType.ClientToWorker;
                }
                else if (invitationSentTo.Contains("Agency") && invitationSentFrom.Contains("Freelancer"))
                {
                    invitationRequestModel.InvitationType = InvitationType.WorkerToAgency;
                }
                else if (invitationSentTo.Contains("Freelancer") && invitationSentFrom.Contains("Agency"))
                {
                    invitationRequestModel.InvitationType = InvitationType.AgencyToWorker;
                }
                var userFound = await _invitationRequestService.GetClientAgency(invitationRequestModel);
                if (userFound == null)
                {
                    var addInvitation = (InvitationRequestModel)(await _invitationRequestService.AddInvitation(invitationRequestModel)).Data;
                    if (addInvitation.Id > 0)
                    {
                        var isUserExit = await _securityService.GetUserDetail(invitationRequestModel.FromUserId);
                        invitationRequestModel.InvitationSentFrom = (UserInfo)isUserExit.Data;
                        message = "Successfully sent invitation to agency.";
                        //await SendEmail(invitationRequest.Id, invitationRequest.InvitationSentFrom.FullName, invitationRequest.InvitationSentFrom.FirstName, invitationRequest.InvitationSentFrom.LastName, invitationRequest.InvitationSentTo.FullName, invitationRequest.InvitationSentFrom.Email, invitationRequest.InvitationSentTo.Email);
                        return new OkObjectResult(invitationRequestModel);
                    }
                }
                else if (userFound != null)
                {
                    message = "Already sent invitation to agency.";
                    // return Request.CreateResponse(HttpStatusCode.NoContent, message);
                    return new NoContentResult();
                    //return (IHttpActionResult)Json(new { Response = HttpStatusCode.NoContent, Message = message });// return Content(HttpStatusCode.NoContent, message);
                }
                return new OkObjectResult(invitationRequestModel);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
    }
}
