using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TimeloggerCore.Data.Repository
{
    public class PackageRepository : BaseRepository<Package, int>, IPackageRepository
    {
        public PackageRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public Task<Package> GetAllNonPackage(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Package> GetAllPackage(string userId)
        {
            var package = await DbContext.Package.Where(x => !x.IsDeleted && x.UserId == userId && x.IsActive).FirstOrDefaultAsync();
            return package;
        }

        //public Package AllPackage(Package package)
        //{
        //     context.Package.Add(package);
        //    return package;

        //}
    }
}
