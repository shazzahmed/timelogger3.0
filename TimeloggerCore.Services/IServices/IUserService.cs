using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Data.Entities;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;
using ApplicationRole = TimeloggerCore.Data.Entities.ApplicationRole;

namespace TimeloggerCore.Services.IService
{
    public interface IUserService : IBaseService<UserModel, ApplicationUser, string>
    {
        //Task<BaseModel> CreateUser(RegisterUserModel model);
        Task<BaseModel> GetAllAgency();
        Task<BaseModel> GetAllFreelancers();
        Task<BaseModel> GetClients();
        Task<BaseModel> GetAgency();
        Task<BaseModel> GetAllWorker();
        Task<List<string>> GetAdminUserRoles();
        Task<List<UserDetailsModel>> GetUsersByRoles(List<string> roles);
        Task<List<UserDetailsModel>> GetUsersByRole(string role);
        Task<bool> IsEmailAlreadyExist(string email);
        Task<bool> IsPhoneAlreadyExist(string phone);
        string GetRolesForUserById(string userId);
        string GetRolesForUserByEmail(string email);
    }
}
