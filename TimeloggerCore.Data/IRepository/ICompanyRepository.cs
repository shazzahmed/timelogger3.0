using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface ICompanyRepository : IBaseRepository<Company, int>
    {
        Task<Company> GetbyUserId(string userId);
        Task<List<Company>> GetCompaniesWithProjects();
    }
}
