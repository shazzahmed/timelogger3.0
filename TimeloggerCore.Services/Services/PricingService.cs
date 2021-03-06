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
    public class PricingService : BaseService<CountryCodeModel, CountryCode, int>, IPricingService
    {
        private readonly IPricingRepository _pricingRepository;

        public PricingService(
            IMapper mapper, 
            IPricingRepository pricingRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, pricingRepository, unitOfWork)
        {
            _pricingRepository = pricingRepository;
        }
        public async Task<BaseModel> GetAllCountryCode()
        {
            var result = await _pricingRepository.GetAllCountryCode();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<CountryCode>, List<CountryCodeModel>>(result)
            };
        }
    }
}
