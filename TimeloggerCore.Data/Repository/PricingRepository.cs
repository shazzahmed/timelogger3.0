using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeloggerCore.Data.Repository
{
    public class PricingRepository : BaseRepository<CountryCode, int>, IPricingRepository
    {
        public PricingRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<CountryCode>> GetAllCountryCode()
        {
            var countryCode = DbContext.CountryCodes;
            return await countryCode.ToListAsync();
        }
    }
}
