using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IPackageRepository : IBaseRepository<Package, int>
    {
        Task<Package> GetAllPackage(string userId);
        Task<Package> GetAllNonPackage(string userId);
        //Package AllPackage(Package package);
    }
}
