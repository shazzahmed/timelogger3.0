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
    public class PermissionService : BaseService<PermissionModel, Permission, int>, IPermissionService
    {
        private readonly IPermissionRepository permissionRepository;

        public PermissionService(IMapper mapper, IPermissionRepository permissionRepository, IUnitOfWork unitOfWork) : base(mapper, permissionRepository, unitOfWork)
        {
            this.permissionRepository = permissionRepository;
        }
    }
}
