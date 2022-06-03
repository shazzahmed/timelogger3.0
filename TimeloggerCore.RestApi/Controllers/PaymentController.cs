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
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        // GET: Api/Payment/AddPayment
        [HttpGet]
        [ActionName("AddPayment")]
        [Route("AddPayment")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> AddPayment(PaymentModel paymentModel)
        {
            try
            {
                var result = await _paymentService.Add(paymentModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/UserSingleInvoice/{InvoiceId}
        [HttpGet]
        [ActionName("UserFirstInvoice")]
        [Route("UserSingleInvoice/{InvoiceId}")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> UserFirstInvoice(int InvoiceId)
        {
            try
            {
                var result = await _paymentService.GetUserSingleInvoice(InvoiceId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/GetAllPayment
        [HttpGet]
        [ActionName("GetAllPayment")]
        [Route("GetAllPayment")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> GetAllPayment()
        {
            try
            {
                var result = await _paymentService.GetAllPayment();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/PaymentVerifyRequest
        [HttpGet]
        [ActionName("PaymentVerifyRequest")]
        [Route("PaymentVerifyRequest")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PaymentVerifyRequest(PaymentInfoModel paymentInfoModel)
        {
            try
            {
                var result = await _paymentService.GetActivePayment(paymentInfoModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/EnquiryRequest
        [HttpGet]
        [ActionName("EnquiryRequest")]
        [Route("EnquiryRequest")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> EnquiryRequest(PaymentInquiryModel paymentInquiryModel)
        {
            try
            {
                var result = await _paymentService.GetActivePayment(paymentInquiryModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/PaypalRecurringPayment
        [HttpGet]
        [AllowAnonymous]
        [ActionName("PaypalRecurringPayment")]
        [Route("PaypalRecurringPayment")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> PaypalRecurringPayment()
        {
            try
            {
                var result = await _paymentService.GetAllRecurringPayment();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/InvoiceGenerate
        [HttpGet]
        [ActionName("InvoiceGenerate")]
        [Route("InvoiceGenerate")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> InvoiceGenerate()
        {
            try
            {
                var result = await _paymentService.GetAllActiveClient();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Failed(message: "There was an error processing your request, please try again. " + ex.Message));
                throw;
            }
        }
        // GET: Api/Payment/ReminderEmail
        [HttpGet]
        [ActionName("ReminderEmail")]
        [Route("ReminderEmail")]
        [Produces("application/json", Type = typeof(BaseModel))]
        public async Task<IActionResult> ReminderEmail()
        {
            try
            {
                var result = await _paymentService.GetAllPendingClient();
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
