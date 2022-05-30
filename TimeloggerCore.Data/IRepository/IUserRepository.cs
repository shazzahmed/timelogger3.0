using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Data.IRepository
{
    public interface IUserRepository : IBaseRepository<ApplicationUser, string>
    {
        Task<List<string>> GetAdminUserRoles();
        string GetRolesForUserById(string userId);
        string GetRolesForUserByEmail(string email);
        Task<bool> IsEmailAlreadyExists(string email);
        Task<bool> IsPhoneAlreadyExists(string phone);
        Task<List<UserDetailsModel>> GetUsersByRoles(List<string> userRoles);
        Task<List<UserDetailsModel>> GetUsersByRole(string userRole);
        Task<List<ApplicationUser>> GetAgencyEmployee(string AgencyId);
        Task<List<ApplicationUser>> GetAllWorker();
        Task<bool> AgencyApproved(string workerId, string agencyId);
    }
}
