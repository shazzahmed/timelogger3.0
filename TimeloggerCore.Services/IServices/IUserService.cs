using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Data.Entities;

namespace TimeloggerCore.Services.IService
{
    public interface IUserService : IBaseService<UserModel, ApplicationUser, string>
    {
        //Task<BaseModel> CreateUser(RegisterUserModel model);
        Task<BaseModel> GetAllAgency();
    }
}
