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
    public class CompanyService : BaseService<CompanyModel, Company, int>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(
            IMapper mapper, 
            ICompanyRepository companyRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, companyRepository, unitOfWork)
        {
            _companyRepository = companyRepository;
        }
        public async Task<BaseModel> GetbyUserId(string userId)
        {
            var result = await _companyRepository.GetbyUserId(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<Company, CompanyModel>(result)
            };
        }
        public async Task<BaseModel> GetCompaniesWithProjects()
        {
            var result = await _companyRepository.GetCompaniesWithProjects();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<Company>,List<CompanyModel>>(result)
            };
        }
    }
}
