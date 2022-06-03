using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface ICompanyService : IBaseService<CompanyModel, Company, int>
    {
        Task<BaseModel> GetbyUserId(string userId);
        Task<BaseModel> GetCompaniesWithProjects();
    }
}
