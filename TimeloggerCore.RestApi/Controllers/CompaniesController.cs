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
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class CompaniesController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(
            ICompanyService companyService
            )
        {
            _companyService = companyService;
        }

        // GET: Api/Companies/GetCompanies
        [HttpGet]
        [ActionName("GetCompanies")]
        [Route("GetCompanies")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var result = await _companyService.Get();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }

        // GET: Api/Companies/GetCompany/{id}
        [HttpGet]
        [ActionName("GetCompany")]
        [Route("GetCompany/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetCompany(string userId)
        {
            try
            {
                var result = await _companyService.GetbyUserId(userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }

        // GET: Api/Companies/GetCompaniesWithProjects
        [HttpGet]
        [ActionName("GetCompaniesWithProjects")]
        [Route("GetCompaniesWithProjects")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetCompaniesWithProjects()
        {
            try
            {
                var result = await _companyService.GetCompaniesWithProjects();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.InnerException.Message));
                throw;
            }
        }

        // GET: Api/Companies/GetCompanyByUser/{userId}
        [HttpGet]
        [ActionName("GetCompanyByUser")]
        [Route("GetCompanyByUser/{userId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetCompanyByUser(string userId)
        {
            try
            {
                var result = await _companyService.Get(where: c => c.UserId == userId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // PUT: Api/Companies/Company
        [HttpGet]
        [ActionName("PutCompany")]
        [Route("Company")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PutCompany(CompanyModel companyModel)
        {
            try
            {
                if (await _companyService.FirstOrDefaultAsync(x=>x.Id == companyModel.Id) != null)
                {
                    await _companyService.Update(companyModel);
                    return new OkObjectResult(companyModel);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Companies/Company
        [HttpPost]
        [ActionName("PostCompany")]
        [Route("Company")]
        [ServiceFilter(typeof(ValidateModelState))]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PostCompany(CompanyModel companyModel)
        {
            try
            {
                var result = await _companyService.Add(companyModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // Delete: Api/Companies/Company
        [HttpDelete]
        [ActionName("DeleteCompany")]
        [Route("Company/{id}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> DeleteCompany(CompanyModel companyModel)
        {
            try
            {
                if (await _companyService.FirstOrDefaultAsync(x => x.Id == companyModel.Id) != null)
                {
                    await _companyService.SoftDelete(companyModel);
                    return new OkObjectResult("Succesful");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
    }
}
