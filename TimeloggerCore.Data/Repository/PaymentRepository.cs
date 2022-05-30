using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Data.Repository
{
    public class PaymentRepository : BaseRepository<Payment, int>, IPaymentRepository
    {
        public PaymentRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<Payment>> GetUserInvoice(string userId)
        {
            var userInvoice = await GetAsync(
                 x =>
                 x.UserId == userId,
                 o => o.OrderBy(x => x.Id),
                 i => i.User, i => i.Package);
            return userInvoice;
        }

        public async Task<Payment> GetUserSingleInvoice(int InvoiceId)
        {
            var userInvoice = await FirstOrDefaultAsync(
                 x =>
                 x.Id == InvoiceId && !x.IsDeleted,
                 null,
                 i => i.Package, i => i.User);
            return userInvoice;
        }
        public async Task<List<Payment>> GetAllPayment()
        {
            var userInvoice = await GetAsync(
                 x =>
                 !x.IsDeleted && x.Package.PackageTypeId != PackageType.Mini,
                 o => o.OrderBy(x => x.Id),
                 i => i.User, i => i.Package);
            return userInvoice;
        }
        public async Task<List<Payment>> GetAllRecurringPayment()
        {
            var userRecurringInvoice = await GetAsync(
                 x =>
                 !x.IsDeleted && x.Package.PackageTypeId != PackageType.Mini && x.IsRecurring && x.IsActive,
                 o => o.OrderBy(x => x.Id),
                 i => i.User, i => i.Package);
            return userRecurringInvoice;
        }
        public async Task<Payment> CurrentActivePayment(string UserId)
        {
            var userInvoice = await FirstOrDefaultAsync(
                 x =>
                 !x.IsDeleted
                 && x.UserId == UserId
                 && x.IsActive,
                 null,
                 i => i.Package, i => i.User);
            return userInvoice;
        }
        public async Task<Payment> GetActivePayment(int paymentId)
        {
            var payment = await FirstOrDefaultAsync(
                 x =>
                 !x.IsDeleted
                 && x.Id == paymentId
                 && !x.IsPaid && x.IsActive,
                 null,
                 i => i.User);
            return payment;
        }
        public async Task<List<Payment>> GetAllActiveClient()
        {
            var allActiveInvoice = await GetAsync(
                 x =>
                 !x.IsDeleted && x.IsActive && x.IsPaid);
            return allActiveInvoice;
        }
        public async Task<List<Payment>> GetAllPendingClient()
        {
            var allActiveInvoice = await GetAsync(
                 x =>
                 !x.IsDeleted && x.IsActive && x.PaymentStatus == PaymentStatus.Pending,
                 o => o.OrderBy(x => x.Id),
                 i => i.User);
            return allActiveInvoice;
        }
        public async Task<Payment> CurrentNonActivePayment(string UserId)
        {
            var userInvoice = await FirstOrDefaultAsync(
                 x =>
                 x.IsDeleted
                 && x.UserId == UserId
                 && !x.IsActive,
                 o => o.OrderBy(x => x.Id),
                 i => i.User, i => i.Package);
            return userInvoice;
        }
    }
}
