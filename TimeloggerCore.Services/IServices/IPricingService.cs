using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IPricingService : IBaseService<CountryCodeModel, CountryCode, int>
    {
        Task<List<CountryCode>> GetAllCountryCode();
    }
}
