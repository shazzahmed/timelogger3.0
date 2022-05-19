﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Common.Options;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;

namespace TimeloggerCore.Services.Services
{
    public class UserService : BaseService<UserModel, ApplicationUser, string>, IUserService
    {
        private readonly IUserRepository _userRepository;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly UserManager<ApplicationUser> _applicationUserManager;

        private readonly TimeloggerCoreOptions _timeloggerCoreOptions;

        //private readonly ISecurityService _securityService;
        //private readonly IStatusService _statusService;
        //private readonly ICompanyService _companyService;
        //private readonly IAddressService _addressService;
        //private readonly INotificationTemplateService _notificationTemplateService;
        //private readonly ICommunicationService _communicationService;

        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,

            IOptionsSnapshot<TimeloggerCoreOptions> timeloggerCoreOptions,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> applicationUserManager

            //ISecurityService securityService,
            //IStatusService statusService,
            //ICompanyService companyService,
            //IAddressService addressService,
            //INotificationTemplateService notificationTemplateService,
            //ICommunicationService communicationService
            ) : base(mapper, userRepository, unitOfWork)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _applicationUserManager = applicationUserManager;

            _timeloggerCoreOptions = timeloggerCoreOptions.Value;

            //_securityService = securityService;
            //_statusService = statusService;
            //_companyService = companyService;
            //_addressService = addressService;
            //_notificationTemplateService = notificationTemplateService;
            //_communicationService = communicationService;
        }


        public async Task<BaseModel> GetAllAgency()
        {
            //_applicationUserManager.Users.Include(u => u.role).ThenInclude(ur => ur.Role).ToList();
            //_applicationUserManager.Users.Include
            var a = _applicationUserManager.GetUsersInRoleAsync("Admin");
            return BaseModel.Succeed(data: a);
            //_userRepository.GetAsync(x=> x.)
        }
        //public async Task<BaseModel> CreateUser(RegisterUserModel model)
        //{
        //    var preactiveStats = await _statusService.FirstOrDefaultAsync(s => s.Name.Equals(UserStatusType.Preactive.ToString()));
        //    if (preactiveStats == null)
        //        throw new Exception($"{UserStatusType.Preactive.ToString()} is not found in the system.");

        //    var result = await _securityService.CreateUser(model.FirstName, model.LastName, model.Email, model.Email, model.PhoneNumber, model.Password, model.ConfirmPassword, model.CreateActivated);
        //    if (!result.Success)
        //        return BaseModel.Failed(result.Message);

        //    var userResult = await _securityService.GetUser(model.Email);
        //    if (!userResult.Success)
        //        return new BaseModel { Success = false, Message = userResult.Message };
        //    var user = (Component.Models.UserClaims)userResult.Data;

        //    var userModel = new UserModel
        //    {
        //        Id = user.Id,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        StatusId = preactiveStats.Id
        //    };
        //    await this.Add(userModel);

        //    var address = new AddressModel
        //    {
        //        UserId = user.Id,
        //        CountryId = model.CountryId,
        //        CityId = model.CityId,
        //        Address = model.Address,
        //        ZipCode = model.ZipCode,
        //        IsDefault = true
        //    };

        //    var addressResult = await _addressService.Add(address);
        //    if (addressResult == null)
        //        return BaseModel.Failed("User added in the system, but address cannot be saved.");

        //    if (!model.CreateActivated)
        //    {
        //        var code = (string)result.Data;
        //        var link = itSolutionOptions.WebUrl + "Account/ConfirmEmail?userId=" + user.Id + "&code=" + HttpUtility.UrlEncode(code);
        //        var template = await _notificationTemplateService.GetNotificationTemplate(NotificationTemplates.EmailUserRegisteration, NotificationTypes.Email);
        //        var emailMessage = template.MessageBody.Replace("#Name", $"{ model.FirstName} { model.LastName}")
        //                                               .Replace("#Link", $"{link}");

        //        var sent = await _communicationService.SendEmail(template.Subject, emailMessage, user.Email);
        //        if (!sent)
        //            return BaseModel.Failed("Confirmation link cannot be sent, plz try again latter");

        //        return new BaseModel { Success = true, Message = "Account created successfully. A confirmation link has been sent to your specified email , click the link to confirm your email and proceed to login." };
        //    }

        //    return BaseModel.Succeed(message: result.Message);
        //}
    }
}
