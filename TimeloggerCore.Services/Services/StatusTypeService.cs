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
    public class StatusTypeService : BaseService<StatusTypeModel, StatusType, int>, IStatusTypeService
    {
        private readonly IStatusTypeRepository statusTypeRepository;

        public StatusTypeService(
            IMapper mapper, 
            IStatusTypeRepository statusTypeRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, statusTypeRepository, unitOfWork)
        {
            this.statusTypeRepository = statusTypeRepository;
        }
    }
}
