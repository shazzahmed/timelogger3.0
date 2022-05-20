using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeloggerCore.Common.Utility;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class ApplicationUser
    {
        [MaxLength(60)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Column(TypeName = "DateTime2")]
        public DateTime BirthDate { get; set; }
        public Enums.Gender Gender { get; set; }
        [MaxLength(500)]
        public string Picture { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(60)]
        public string Language { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedAtUtc { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DisabledDate { get; set; }
        [Required]
        public string TimeZoneId { get; set; }

        public bool IsWorkerHasAgency { get; set; }
        public string AgencyId { get; set; }
        public bool IsAgencyApproved { get; set; }
        public int? CompanyId { get; set; }
        public int? StatusId { get; set; }
        public TwoFactorTypes TwoFactorTypeId { get; set; }



        public TwoFactorType TwoFactorType { get; set; }
        public CompanyModel Company { get; set; }

        public StatusModel Status { get; set; }

        public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<TimeLogModel> TimeLogs { get; set; }

        public virtual ICollection<InvitationModel> ClientInvitations { get; set; }
        public virtual ICollection<InvitationModel> UserInvitations { get; set; }

        [ForeignKey("TimeZoneId")]
        public virtual TimeZoneModel TimeZone { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
    public class ApplicationRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
    public class TwoFactorType
    {
        public TwoFactorTypes Id { get; set; }
        public string Name { get; set; }
    }
    public class UserClaimResponse
    {
        public string name { get; set; }
    }

    public class ExternalAuthenticationProvider
    {
        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string HandlerType { get; set; }
    }

    public class ExternalLoginViewModel
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public bool IsPersistent { get; set; }
        public bool BypassTwoFactor { get; set; }
    }

    public class RegisterExternalViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Name { get; set; }
        public string ReturnUrl { get; set; }
        public string State { get; set; }
    }

    public class SendCodeViewModel
    {
        public string UserName { get; set; }
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        public string UserName { get; set; }

        //[Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember this machine?")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RecoveryCodesViewModel
    {
        public string[] RecoveryCodes { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<ExternalAuthenticationProvider> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        public string StatusMessage { get; set; }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public List<ExternalAuthenticationProvider> ExternalAuthenticationProviders { get; set; }
    }

    public class RegisterUserViewModel
    {
        [Display(Name = "FirstName")]
        [StringLength(30, MinimumLength = 0, ErrorMessage = "The first name must contain less than 30 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [StringLength(30, MinimumLength = 0, ErrorMessage = "The last name must contain less than 30 characters.")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "NicNumber")]
        [Required]
        public string NicNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "We need the cell phone to continue.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "The password must be between {2} and {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool CreateActivated { get; set; }
    }

    public class RegisterMerchantViewModel
    {
        // Company info
        [MaxLength(255, ErrorMessage = "Company name cannot be more than 255 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "We need the name of the company.")]
        public string CompanyName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid company phone number.")]
        public string CompanyPhone { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "We need the address of the company to continue.")]
        [MaxLength(100, ErrorMessage = "Company address must be less than 100 characters.")]
        public string CompanyAddress { get; set; }

        public string CompanyZipCode { get; set; }

        [MaxLength(255, ErrorMessage = "Company website cannot have more than 255 characters.")]
        [Url(ErrorMessage = "The URL is not valid.")]
        [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?", ErrorMessage = "The URL is not valid. e.g http://google.com")]
        public string CompanyWebsite { get; set; }


        // User info
        [Required(ErrorMessage = "We need the name of the manager to continue.")]
        [MaxLength(30, ErrorMessage = "Name cannot have more than 30 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "We need the manager's last name to continue.")]
        [MaxLength(30, ErrorMessage = "Last name cannot have more than 30 characters.")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "We need the email to continue.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "We need the password to continue.")]
        [StringLength(14, ErrorMessage = "The password must be between {2} and {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "We need you to confirm the password to continue.")]
        [Compare("Password", ErrorMessage = "New password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool CreateActivated { get; set; }
    }

    public class ConfirmEmailViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Verification code can't be null")]
        public string Code { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ManageLoginsViewModel
    {
        //public IList<UserLoginInfo> CurrentLogins { get; set; }
        //public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }



    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("roles")]
        public string Roles { get; set; }

        [JsonProperty("error")]
        public string ErrorHeader { get; set; }

        [JsonProperty("error_description")]
        public string Error { get; set; }

    }

    public class AddLoginViewModel
    {
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
    }

    public class RemoveLoginViewModel
    {
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }

    public class AuthenticatorViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Verification Code")]
        public string Code { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
        public string SharedKey { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
        public string AuthenticatorUri { get; set; }
    }

    public class UserClaim
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public string NicNumber { get; set; }
        public List<string> Roles { get; set; }
    }
    public class UserDetailModel
    {
        public UserDetailModel()
        {
            Roles = new List<string>();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NicNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool HasPassword { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string TwoFactorType { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        public List<string> Roles { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
    }

    public class UpdateUserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Picture { get; set; }
    }

    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class AccountDetail
    {
        public static Token TokenInfo { get; set; }

        public static UserClaim UserInfo { get; set; }

        public static LoginViewModel LoginInfo { get; set; }

        public static List<Role> Roles { get; set; }

        public static List<ExternalLoginViewModel> ExternalLoginUrls { get; set; }
    }

    public class AuthenticatorModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Verification Code")]
        public string Code { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
        public string SharedKey { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
        public string AuthenticatorUri { get; set; }
    }

    public class ConfirmEmailModel
    {
        [Required]
        public string UserId { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Verification code can't be null")]
        public string Code { get; set; }
    }

    public class PasswordResetModel
    {

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Reset code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and repeat password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class PasswordChangeModel
    {
        [Required]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New password")]
        [Compare("NewPassword", ErrorMessage = "The new password and repeat password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class SetPasswordModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ExternalLoginModel
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public bool IsPersistent { get; set; }
        public bool BypassTwoFactor { get; set; }
    }

    public class RegisterExternalModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
    }

    public class RegisterUserModel
    {
        [Display(Name = "FirstName")]
        [StringLength(30, MinimumLength = 0, ErrorMessage = "The first name must contain less than 30 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [StringLength(30, MinimumLength = 0, ErrorMessage = "The last name must contain less than 30 characters.")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        //[Remote("IsUserExists", "Account", ErrorMessage = "Email address is already in use.")]
        [RegularExpression("[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})", ErrorMessage = "Please enter a valid email address.")]
        [Required]
        public string Email { get; set; }

        public TimeZoneModel TimeZone { get; set; }
        [Required]
        [Display(Name = "Time Zone")]
        public string TimeZoneId { get; set; }

        public List<TimeZoneModel> TimeZones { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "We need the cell phone to continue.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }

        [MaxLength(100, ErrorMessage = "Address must be less than 100 characters.")]
        public string Address { get; set; }

        public string ZipCode { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The password must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,15}$",
        ErrorMessage = "At least 6 characters, one uppercase and lowercase")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool CreateActivated { get; set; }
        public List<RoleModel> UserRoles { get; set; }
        [Required(ErrorMessage = "Please select one of the above")]
        [Display(Name = "User Role")]
        public string UserRole { get; set; }
        public string Picture { get; set; }
        public bool IsWorkerHasAgency { get; set; }
        [Required(ErrorMessage = "Worker agency is requied.")]
        public string AgencyId { get; set; }
        public string WorkerType { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Agree Terms must be checked.")]
        public bool AgreeTerms { get; set; }
        public CompanyModel Company { get; set; }
        public string ConfirmEmailURL { get; set; }
        public string WorkerEmailURL { get; set; }
        public List<ExternalLoginListViewModel> ExternalLoginURLs { get; set; }
    }

    public class RegisterMerchantModel
    {
        // Company info
        [MaxLength(255, ErrorMessage = "Company name cannot be more than 255 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "We need the name of the company.")]
        public string CompanyName { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "We need the company phone to continue.")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid company phone number.")]
        public string CompanyPhone { get; set; }

        // [Required(AllowEmptyStrings = false, ErrorMessage = "We need the address of the company to continue.")]
        [MaxLength(100, ErrorMessage = "Company address must be less than 100 characters.")]
        public string CompanyAddress { get; set; }

        public string CompanyZipCode { get; set; }

        [MaxLength(255, ErrorMessage = "Company website cannot have more than 255 characters.")]
        [Url(ErrorMessage = "The URL is not valid.")]
        [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?", ErrorMessage = "The URL is not valid. e.g http://google.com")]
        public string CompanyWebsite { get; set; }


        // User info
        [Required(ErrorMessage = "We need the name of the manager to continue.")]
        [MaxLength(30, ErrorMessage = "Name cannot have more than 30 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "We need the manager's last name to continue.")]
        [MaxLength(30, ErrorMessage = "Last name cannot have more than 30 characters.")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "We need the email to continue.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        // [Required(AllowEmptyStrings = false, ErrorMessage = "We need the cell phone to continue.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "We need the password to continue.")]
        [StringLength(14, ErrorMessage = "The password must be between {2} and {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "We need you to confirm the password to continue.")]
        [Compare("Password", ErrorMessage = "New password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool CreateActivated { get; set; }
    }

    public class UserLoginInfo
    {
        public string Provider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }
    }

    public class ActivationModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }

    public class ResetModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
    }

    public class ResetPasswordByAdminModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The NewPassword and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class SendCodeModel
    {
        public string SelectedProvider { get; set; }

        //public ICollection<System.Web.Mvc.SelectListItem> Provider { get; set; }
        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }

    public class VerifyCodeModel
    {
        public string UserName { get; set; }

        //[Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember this machine?")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
    public class ChangeEmailModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AddPhoneNumberModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class VerifyPhoneNumberModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class AddLoginModel
    {
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
    }

    public class RemoveLoginModel
    {
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }
}
