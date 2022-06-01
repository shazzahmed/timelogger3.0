using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IPackageService : IBaseService<PackageModel, Package, int>
    {
        Task<BaseModel> GetAllPackage(string userId);
        Task<BaseModel> GetAllNonPackage(string userId);
    }
}
