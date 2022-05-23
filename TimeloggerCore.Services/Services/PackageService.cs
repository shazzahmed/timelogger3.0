using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services
{
    public class PackageService : BaseService<PackageModel, Package, int>, IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IMapper mapper, IPackageRepository packageRepository, IUnitOfWork unitOfWork) : base(mapper, packageRepository, unitOfWork)
        {
            _packageRepository = packageRepository;
        }
        public Task<Package> GetAllPackage(string userId)
        {
            return _packageRepository.GetAllPackage(userId);
        }
    }
}
