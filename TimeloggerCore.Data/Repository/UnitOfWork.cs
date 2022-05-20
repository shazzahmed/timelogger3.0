using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlServerDbContext _dbContext;

        public UnitOfWork(ISqlServerDbContext context)
        {
            _dbContext = context;
        }

        public IPaymentRepository PaymentRepository => new PaymentRepository(_dbContext);

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
