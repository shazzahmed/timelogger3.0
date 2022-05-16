using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services
{
    public class CompanyService : BaseService<CompanyModel, Company, int>, ICompanyService
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository, IUnitOfWork unitOfWork) : base(mapper, companyRepository, unitOfWork)
        {
            this.companyRepository = companyRepository;
        }
    }
}
