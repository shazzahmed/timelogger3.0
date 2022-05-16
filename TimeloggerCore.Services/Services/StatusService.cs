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
    public class StatusService : BaseService<StatusModel, Status, int>, IStatusService
    {
        private readonly IStatusRepository statusRepository;

        public StatusService(IMapper mapper, IStatusRepository statusRepository, IUnitOfWork unitOfWork) : base(mapper, statusRepository, unitOfWork)
        {
            this.statusRepository = statusRepository;
        }
    }
}
