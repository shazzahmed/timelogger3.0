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
    public class PreviousPasswordService : BaseService<PreviousPasswordModel, PreviousPassword, int>, IPreviousPasswordService
    {
        protected readonly IPreviousPasswordsRepository _previousPasswordsRepository;

        public PreviousPasswordService(
            IMapper mapper, 
            IPreviousPasswordsRepository previousPasswordsRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, previousPasswordsRepository, unitOfWork)
        {
            this._previousPasswordsRepository = previousPasswordsRepository;
        }
        public async Task<BaseModel> AddPreviousPassword(PreviousPasswordModel previousPasswordModel)
        {
            var previousPassword = await Add(previousPasswordModel);
            return new BaseModel
            {
                Success = previousPassword.UserId == "" ?true : false,
                Data = previousPassword
            };
        }
    }
}
