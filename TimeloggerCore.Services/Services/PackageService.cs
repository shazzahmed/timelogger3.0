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

        public PackageService(
            IMapper mapper, 
            IPackageRepository packageRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, packageRepository, unitOfWork)
        {
            _packageRepository = packageRepository;
        }
        public async Task<BaseModel> GetAllPackage(string userId)
        {
            var result = await _packageRepository.GetAllPackage(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Package, PackageModel>(result)
            };
        }
        public async Task<BaseModel> GetAllNonPackage(string userId)
        {
            var result = await _packageRepository.GetAllNonPackage(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Package, PackageModel>(result)
            };
        }
    }
}
