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

namespace TimeloggerCore.RestApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ClientAgencyController : BaseController
    {
        private readonly IInvitationRequestService _invitationRequestService;
        private readonly IAgencyService _agencyService;

        public ClientAgencyController(
            IInvitationRequestService invitationRequestService,
            IAgencyService agencyService
            )
        {
            _invitationRequestService = invitationRequestService;
            _agencyService = agencyService;
        }

        // GET: Api/ClientAgency/GetClientAgency/id
        [HttpGet]
        [Authorize]
        [ActionName("GetClientAgency")]
        [Route("GetClientAgency/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetClientAgency(string userId)
        {
            try
            {
                var result = await _invitationRequestService.GetOnlyClientAgencies(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }

        // GET: Api/ClientAgency/GetAgencyClients/{id}
        [HttpGet]
        [Authorize]
        [ActionName("GetAgencyClients")]
        [Route("GetAgencyClients/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAgencyClients(string userId)
        {
            try
            {
                var result = await _agencyService.GetAgencyClients(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }

        // Post: Api/ClientAgency/AddClientAgency
        [HttpPost]
        [Authorize]
        [ActionName("AddClientAgency")]
        [Route("AddClientAgency")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddClientAgency(ClientAgencyModel clientAgencyModel)
        {
            try
            {
                //await _agencyService.Add(clientAgencyModel);
                var result = await _agencyService.AddClientAgency(clientAgencyModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.InnerException.Message));
                throw;
            }
        }

        // GET: Api/ClientAgency/GetSingleClientAgencies/{id}
        [HttpGet]
        [Authorize]
        [ActionName("GetSingleClientAgencies")]
        [Route("GetSingleClientAgencies/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetSingleClientAgencies(string userId)
        {
            try
            {
                var result = await _agencyService.GetSingleClientAgencies(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/ClientAgency/GetAgencyEmployee/{id}
        [HttpGet]
        [Authorize]
        [ActionName("GetAgencyEmployee")]
        [Route("GetAgencyEmployee/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAgencyEmployee(string agencyId)
        {
            try
            {
                var result = await _agencyService.GetAgencyEmployee(agencyId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/ClientAgency/GetAllWorker/{id}
        [HttpGet]
        [Authorize]
        [ActionName("GetAllWorker")]
        [Route("GetAllWorker/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAllWorker()
        {
            try
            {
                var result = await _agencyService.GetAllWorker();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/ClientAgency/ConfirmAgency/{id}
        [HttpGet]
        [Authorize]
        [ActionName("ConfirmAgency")]
        [Route("ConfirmAgency/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ConfirmAgency(string userId)
        {
            try
            {
                    var result = await _agencyService.ConfirmAgency(userId);
                    return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/ClientAgency/ConfirmWorker/{id}
        [HttpGet]
        [Authorize]
        [ActionName("ConfirmWorker")]
        [Route("ConfirmWorker/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ConfirmWorker(string userId)
        {
            try
            {
                var result = await _agencyService.ConfirmWorker(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/ClientAgency/AgencyClientFound
        [HttpGet]
        [Authorize]
        [ActionName("AgencyClientFound")]
        [Route("AgencyClientFound")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AgencyClientFound(ClientAgencyModel clientAgencyModel)
        {
            try
            {
                var result = await _agencyService.GetClientAgency(clientAgencyModel);
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
