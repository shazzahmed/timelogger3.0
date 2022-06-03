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
    public class PackageController : BaseController
    {
        private readonly IPackageService _packageService;
        private readonly IPaymentService _paymentService;

        public PackageController(
            IPackageService packageService,
            IPaymentService paymentService
            )
        {
            _packageService = packageService;
            _paymentService = paymentService;
        }


        // GET: Api/Package/AddPackage
        [HttpGet]
        [ActionName("AddPackage")]
        [Route("AddPackage")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddPackage(PackageModel packageModel)
        {
            try
            {
                var result = await _packageService.Add(packageModel);
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
