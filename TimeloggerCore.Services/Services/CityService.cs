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
    public class CityService : BaseService<CityModel, City, int>, ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(
            IMapper mapper, 
            ICityRepository cityRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, cityRepository, unitOfWork)
        {
            this.cityRepository = cityRepository;
        }
    }
}
