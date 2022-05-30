using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IPaymentRepository : IBaseRepository<Payment, int>
    {
        Task<List<Payment>> GetUserInvoice(string userId);
        Task<Payment> GetUserSingleInvoice(int InvoiceId);
        Task<List<Payment>> GetAllPayment();
        Task<Payment> CurrentActivePayment(string UserId);
        Task<Payment> CurrentNonActivePayment(string UserId);
        Task<Payment> GetActivePayment(int paymentId);
        Task<List<Payment>> GetAllActiveClient();
        Task<List<Payment>> GetAllRecurringPayment();
        Task<List<Payment>> GetAllPendingClient();
    }
}
