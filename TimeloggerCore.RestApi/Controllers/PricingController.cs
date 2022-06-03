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
    public class PricingController : BaseController
    {
        private readonly IPricingService _pricingService;

        public PricingController(
            IPricingService pricingService
            )
        {
            _pricingService = pricingService;
        }


        // GET: Api/Pricing/GetCountryCodes
        [HttpGet]
        [ActionName("AddPackage")]
        [Route("AddPackage")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddPackage()
        {
            try
            {
                var result = await _pricingService.GetAllCountryCode();
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
