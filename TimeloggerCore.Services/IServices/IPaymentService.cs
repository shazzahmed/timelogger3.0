using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;

namespace TimeloggerCore.Services.IService
{
    public interface IPaymentService : IBaseService<PaymentModel, Payment, int>
    {
        Task<BaseModel> GetUserInvoice(string userId);
        Task<BaseModel> GetUserSingleInvoice(int InvoiceId);
        Task<BaseModel> GetAllPayment();
        Task<BaseModel> GetAllRecurringPayment();
        Task<BaseModel> CurrentActivePayment(string UserId);
        Task<BaseModel> GetActivePayment(int paymentId);
        Task<BaseModel> GetAllActiveClient();
        Task<BaseModel> GetAllPendingClient();
        Task<BaseModel> CurrentNonActivePayment(string UserId);
    }
}
