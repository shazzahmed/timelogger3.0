using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.Repository
{
    public class CompanyRepository : BaseRepository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<Company> GetbyUserId(string userId)
        {
            var company = await FirstOrDefaultAsync(
                 x => x.UserId == userId);
            return company;
        }
        public async Task<List<Company>> GetCompaniesWithProjects()
        {
            var companies = await GetAsync(
                 null,
                 null,
                 i => i.Projects);
            return companies;
        }
    }
}
