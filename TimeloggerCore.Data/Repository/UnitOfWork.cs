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

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var result = await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
