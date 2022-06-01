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
    public class CountryService : BaseService<CountryModel, Country, int>, ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(
            IMapper mapper, 
            ICountryRepository countryRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, countryRepository, unitOfWork)
        {
            this.countryRepository = countryRepository;
        }
    }
}
