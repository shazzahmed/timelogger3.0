using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Data.Entities;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TimeloggerCore.Data.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser, string>, IUserRepository
    {
        public UserRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<string>> GetAdminUserRoles()
        {
            var otherRoles = new List<string> { "", "" };
            //var roles = await DbContext.Roles.Where(x => !otherRoles.Contains(x.Name)).Select(x => x.Name).ToListAsync();
            var roles = await DbContext.User.Where(x => !otherRoles.Contains(x.UserName)).Select(x => x.UserName).ToListAsync();
            return roles;
        }
        public async Task<List<UserDetailsModel>> GetUsersByRole(string userRole)
        {
            var role = await DbContext.User.FirstOrDefaultAsync(x => x.UserName.Equals(userRole));
            return (await DbContext.User.Where(x => x.UserRoles.Any(c => c.RoleId.Equals(role.Id))).ToListAsync())
                            .Select(x => new UserDetailsModel
                            {
                                Id = x.Id,
                                Email = x.Email,
                                Name = x.FirstName + " " + x.LastName
                            }).ToList();
        }

        public async Task<List<UserDetailsModel>> GetUsersByRoles(List<string> userRoles)
        {
            var roles = await DbContext.User.Where(x => userRoles.Contains(x.UserName)).Select(c => c.Id).ToListAsync();
            var result = (await DbContext.User.Where(x => x.UserRoles.Any(c => roles.Contains(c.RoleId))).ToListAsync())
                            .Select(x => new UserDetailsModel
                            {
                                Id = x.Id,
                                Email = x.Email,
                                Name = x.FirstName + " " + x.LastName
                            }).ToList();
            return result;
        }
        public string GetRolesForUserById(string userId)
        {
            //using (
            //    var userManager =
            //        new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            //{
            //    var rolesForUser = userManager.GetRoles(userId);
            //    return rolesForUser.FirstOrDefault();
            //}
            return "";
        }
        public string GetRolesForUserByEmail(string email)
        {
            //using (
            //    var userManager =
            //        new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            //{
            //    var rolesForUserbyemail = userManager.FindByEmail(email);
            //    var rolesForUser = userManager.GetRoles(rolesForUserbyemail.Id);
            //    return rolesForUser.FirstOrDefault();
            //}
            return "";
        }
        public async Task<bool> IsEmailAlreadyExists(string email)
        {
            return await DbContext.User.AnyAsync(x => x.Email.Equals(email) || x.UserName.Equals(email));
        }

        public async Task<bool> IsPhoneAlreadyExists(string phone)
        {
            return await DbContext.User.AnyAsync(x => x.PhoneNumber.Equals(phone));
        }
        public async Task<List<ApplicationUser>> GetAgencyEmployee(string AgencyId)
        {
            var agencyWorker = await GetAsync(x => x.AgencyId == AgencyId && x.IsWorkerHasAgency && x.IsAgencyApproved);
            //&& x.IsWorkerHasAgency&&x.IsAgencyApproved
            return agencyWorker;

        }
        public async Task<List<ApplicationUser>> GetAllWorker()
        {
            var worker = await GetAsync(x => !x.IsWorkerHasAgency);
            return worker;
        }
        public async Task<bool> AgencyApproved(string workerId, string agencyId)
        {
            var user = await FirstOrDefaultAsync(
                x => 
                x.Id == workerId && x.AgencyId == agencyId);
            user.IsAgencyApproved = true;
            await UpdateAsync(user);
            return true;
        }
    }
}
