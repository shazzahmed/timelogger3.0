using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IPricingRepository : IBaseRepository<CountryCode, int>
    {
        Task<List<CountryCode>> GetAllCountryCode();
    }
}
