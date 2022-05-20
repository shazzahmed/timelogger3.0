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
            var userInvoice = await DbContext.Payment.Where(x => x.UserId == userId).Include(x => x.User).Include(x => x.Package).ToListAsync();
            return userInvoice;
        }

        public async Task<Payment> GetUserSingleInvoice(int InvoiceId)
        {
            var userInvoice = await DbContext.Payment.Where(x => x.Id == InvoiceId && !x.IsDeleted).Include(x => x.Package).Include(x => x.User).FirstOrDefaultAsync();
            return userInvoice;
        }
        public async Task<ApplicationUser> UserInfo(string userId)
        {
            var user = await DbContext.User.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user;
        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await DbContext.User.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }
        public async Task<bool> AgencyApproved(string workerId, string agencyId)
        {
            var user = await DbContext.User.Where(x => x.Id == workerId && x.AgencyId == agencyId).FirstOrDefaultAsync();
            user.IsAgencyApproved = true;
            DbContext.Entry(user).State = EntityState.Modified;
            DbContext.SaveChanges();
            return true;

        }
        public async Task<List<Payment>> GetAllPayment()
        {
            var userInvoice = await DbContext.Payment.Where(x => !x.IsDeleted && x.Package.PackageTypeId != PackageType.Mini).Include(x => x.Package).Include(x => x.User).ToListAsync();
            return userInvoice;
        }
        public async Task<List<Payment>> GetAllRecurringPayment()
        {
            var userRecurringInvoice = await DbContext.Payment.Where(x => !x.IsDeleted && x.Package.PackageTypeId != PackageType.Mini && x.IsRecurring && x.IsActive).Include(x => x.Package).Include(x => x.User).ToListAsync();
            return userRecurringInvoice;
        }
        public async Task<Payment> CurrentActivePayment(string UserId)
        {
            var userInvoice = await DbContext.Payment.Where(x => !x.IsDeleted
            && x.UserId == UserId
            && x.IsActive).Include(x => x.Package).Include(x => x.User).FirstOrDefaultAsync();
            return userInvoice;
        }
        public async Task<Payment> GetActivePayment(int paymentId)
        {
            var payment = await DbContext.Payment.Where(
            x => !x.IsDeleted
            && x.Id == paymentId
           && !x.IsPaid && x.IsActive).Include(x => x.User).FirstOrDefaultAsync();
            return payment;
        }
        public async Task<List<Payment>> GetAllActiveClient()
        {
            var allActiveInvoice = await DbContext.Payment.Where(x => !x.IsDeleted && x.IsActive && x.IsPaid).ToListAsync();
            return allActiveInvoice;
        }
        public async Task<List<Payment>> GetAllPendingClient()
        {
            var allActiveInvoice = await DbContext.Payment.Where(x => !x.IsDeleted && x.IsActive && x.PaymentStatus == PaymentStatus.Pending).Include(d => d.User).ToListAsync();
            return allActiveInvoice;
        }
        public async Task<Payment> CurrentNonActivePayment(string UserId)
        {
            var userInvoice = await DbContext.Payment.OrderByDescending(x => x.Id).Where(x => x.IsDeleted
                && x.UserId == UserId
                && !x.IsActive).Include(x => x.Package).Include(x => x.User).FirstOrDefaultAsync();
            return userInvoice;
        }
    }
}
