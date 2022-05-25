﻿using TimeloggerCore.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Options;
using TimeloggerCore.Common.Utility;
using TimeloggerCore.Common.Utility.Constants;
using static TimeloggerCore.Common.Utility.Enums;
using StatusType = TimeloggerCore.Data.Entities.StatusType;
using TimeZone = TimeloggerCore.Data.Entities.TimeZone;

namespace TimeloggerCore.Data.Database
{
    public static class DataSeeder
    {
        public static async Task Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SqlServerDbContext>();
                var componentOptions = scope.ServiceProvider.GetService<Microsoft.Extensions.Options.IOptionsSnapshot<ComponentOptions>>();


                #region Statuses StatusTypes

                var statusTypes = new List<StatusType>()
                {
                    new StatusType() { Id = StatusTypes.UserStatus, Name = "User statuses" },
                    new StatusType() { Id = StatusTypes.NotificationStatus, Name = "Notification statuses" }
                };
                var newStatusTypes = statusTypes.Where(s => !context.StatusTypes.Select(st => st.Id).Contains(s.Id));
                if (newStatusTypes.Any())
                {
                    context.StatusTypes.AddRange(newStatusTypes);
                    context.SaveChanges();
                }

                var statuses = new List<Status>()
                {
                    // User statuses
                    new Status() { Name = UserStatus.Preactive.ToString(), Code = UserStatusCode.Preactive, TypeId = StatusTypes.UserStatus },
                    new Status() { Name = UserStatus.Active.ToString(), Code = UserStatusCode.Active, TypeId = StatusTypes.UserStatus },
                    new Status() { Name = UserStatus.Inactive.ToString(), Code = UserStatusCode.Inactive, TypeId = StatusTypes.UserStatus },
                    new Status() { Name = UserStatus.Canceled.ToString(), Code = UserStatusCode.Canceled, TypeId = StatusTypes.UserStatus },
                    new Status() { Name = UserStatus.Frozen.ToString(), Code = UserStatusCode.Frozen, TypeId = StatusTypes.UserStatus },
                    new Status() { Name = UserStatus.Blocked.ToString(), Code = UserStatusCode.Blocked, TypeId = StatusTypes.UserStatus },

                    // Notification statuses
                    new Status() { Name = NotificationStatus.Created.ToString(), Code = NotificationStatusCode.Created, TypeId = StatusTypes.NotificationStatus },
                    new Status() { Name = NotificationStatus.Queued.ToString(), Code = NotificationStatusCode.Queued, TypeId = StatusTypes.NotificationStatus },
                    new Status() { Name = NotificationStatus.Succeeded.ToString(), Code = NotificationStatusCode.Succeeded, TypeId = StatusTypes.NotificationStatus },
                    new Status() { Name = NotificationStatus.Failed.ToString(), Code = NotificationStatusCode.Failed, TypeId = StatusTypes.NotificationStatus },
                };
                var newStatuses = statuses.Where(s => !context.Statuses.Select(st => st.Name).Contains(s.Name));
                if (newStatuses.Any())
                {
                    context.Statuses.AddRange(newStatuses);
                    context.SaveChanges();
                }

                #endregion Statuses StatusTypes


                #region Countries Cities

                var countries = new List<Country>()
                {
                    new Country(){ Code="0092", Name="Pakistan" },
                };
                var newCountries = countries.Where(c => !context.Countries.Select(cn => cn.Code).Contains(c.Code));
                if (newCountries.Any())
                {
                    context.Countries.AddRange(newCountries);
                    context.SaveChanges();
                }

                var cities = new List<City>()
                {
                    new City(){ Code="051", Name="Islamabad" },
                    new City(){ Code="021", Name="Karachi" },
                    new City(){ Code="052", Name="Lahore" },
                    new City(){ Code="091", Name="Peshawar" }
                };
                var newCities = cities.Where(c => !context.Cities.Select(ct => ct.Code).Contains(c.Code));
                if (newCities.Any())
                {
                    context.Cities.AddRange(newCities);
                    context.SaveChanges();
                }

                #endregion Countries Cities


                #region companies

                var country = context.Countries.FirstOrDefault(c => c.Name.Equals("Pakistan"));
                if (country == null)
                {
                    throw new Exception($"{nameof(country)} is not found, seeds are malfunctioned.");
                }

                var companies = new List<Company>()
                {
                    new Company()
                    {
                        Name = "KFC",
                        Phone = "123",
                        Website = "xyz"
                    },
                    new Company()
                    {
                        Name = "BFC",
                        Phone = "123",
                        Website = "xyz"
                    }
                };
                var newCompanies = companies.Where(c => !context.Companies.Select(co => co.Name).Contains(c.Name));
                if (newCompanies.Any())
                {
                    context.Companies.AddRange(newCompanies);
                    context.SaveChanges();

                    var city = context.Cities.FirstOrDefault(c => c.Name.Equals("Islamabad"));
                    if (city == null)
                    {
                        throw new Exception($"{nameof(city)} is not found, seeds are malfunctioned.");
                    }
                    foreach (var company in newCompanies)
                    {
                        var address = new Addresses
                        {
                            CompanyId = company.Id,
                            Address = "Tariq road",
                            CountryId = country.Id,
                            CityId = city.Id,
                            IsDefault = true
                        };
                        var addr = context.Addresses.AddAsync(address).Result;
                    }
                    context.SaveChanges();
                }


                

                #endregion companies


                #region  Users Roles UserRoles TwoFactorTypes

                if (componentOptions.Value.Security.SecurityService == "AspnetIdentity")
                {
                    var roles = Enum.GetValues(typeof(UserRoles));
                    var roleStore = new RoleStore<ApplicationRole>(context);
                    foreach (var role in roles)
                    {
                        if (!context.Roles.Any(r => r.Name.ToLower() == role.ToString()))
                        {
                            context.Roles.Add(new ApplicationRole { Name = role.ToString(), NormalizedName = role.ToString().ToUpper() });
                            context.SaveChanges();
                        }
                    }

                    var twoFactorTypes = new List<TwoFactorType>()
                    {
                        new TwoFactorType() { Id = Enums.TwoFactorTypes.None, Name = TimeloggerCore.Common.Utility.Constants.TwoFactorTypes.None },
                        new TwoFactorType() { Id = Enums.TwoFactorTypes.Email, Name = TimeloggerCore.Common.Utility.Constants.TwoFactorTypes.Email },
                        new TwoFactorType() { Id = Enums.TwoFactorTypes.Phone, Name = TimeloggerCore.Common.Utility.Constants.TwoFactorTypes.Phone },
                        new TwoFactorType() { Id = Enums.TwoFactorTypes.Authenticator, Name = TimeloggerCore.Common.Utility.Constants.TwoFactorTypes.Authenticator }
                    };
                    var newTwoFactorType = twoFactorTypes.Where(t => !context.TwoFactorTypes.Select(tf => tf.Name).Contains(t.Name));
                    if (newTwoFactorType.Any())
                    {
                        context.TwoFactorTypes.AddRange(newTwoFactorType);
                        context.SaveChanges();
                    }
                    var timeZones = new List<TimeZone>()
                    {
                        new TimeZone() { Id = "Afghanistan Standard Time", DisplayName = "(UTC+04:30) Kabul", StandardName = "Afghanistan Standard Time" },
                        new TimeZone() { Id = "Alaskan Standard Time", DisplayName = "(UTC-09:00) Alaska", StandardName = "Alaskan Standard Time" },
                        new TimeZone() { Id = "Aleutian Standard Time", DisplayName = "(UTC-10:00) Aleutian Islands", StandardName = "Aleutian Standard Time" },
                        new TimeZone() { Id = "Pakistan Standard Time", DisplayName = "(UTC+05:00) Islamabad, Karachi", StandardName = "Pakistan Standard Time" },
                    };
                    var newtimeZones = timeZones.Where(t => !context.TimeZone.Select(tf => tf.StandardName).Contains(t.StandardName));
                    if (newtimeZones.Any())
                    {
                        context.TimeZone.AddRange(newtimeZones);
                        context.SaveChanges();
                    }
                    var twoFactorType = context.TwoFactorTypes.FirstOrDefault(t => t.Id == Enums.TwoFactorTypes.None);
                    var timeZone = context.TimeZone.FirstOrDefault(t => t.StandardName == "Pakistan Standard Time");
                    if (twoFactorType == null)
                    {
                        throw new Exception($"{nameof(twoFactorType)} is not found, seeds are malfunctioned.");
                    }

                    var status = context.Statuses.FirstOrDefault(s => s.Name.Equals(UserStatus.Active.ToString()));
                    if (status == null)
                    {
                        throw new Exception($"{nameof(status)} is not found, seeds are malfunctioned.");
                    }

                    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var user = new ApplicationUser
                    {
                        Email = "admin@itsolution.com",
                        UserName = "admin@itsolution.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "admin@itsolution.com",
                        NormalizedUserName = "admin@itsolution.com",
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        TwoFactorTypeId = twoFactorType.Id,
                        PhoneNumber = "03213828130",
                        PhoneNumberConfirmed = true,
                        StatusId = status.Id,
                        TimeZoneId = timeZone.Id
                    };
                    if (!context.Users.Any(u => u.Email == user.Email))
                    {
                        var result = _userManager.CreateAsync(user, "ItSolution1@345").Result;

                        var ro = _userManager.AddToRoleAsync(user, UserRoles.SuperAdmin.ToString()).Result;

                        var city = context.Cities.FirstOrDefault(c => c.Name.Equals("Islamabad"));
                        if (city == null)
                        {
                            throw new Exception($"{nameof(city)} is not found, seeds are malfunctioned.");
                        }
                        var address = new Addresses
                        {
                            UserId = user.Id,
                            Address = "Tariq road",
                            CountryId = country.Id,
                            CityId = city.Id,
                            IsDefault = true
                        };
                        var addr = context.Addresses.AddAsync(address).Result;
                        context.SaveChanges();
                    }

                    var customer = new ApplicationUser
                    {
                        Email = "itsolution.customer@yopmail.com",
                        UserName = "itsolution.customer@yopmail.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "itsolution.customer@yopmail.com",
                        NormalizedUserName = "itsolution.customer@yopmail.com",
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        TwoFactorTypeId = twoFactorType.Id,
                        PhoneNumber = "03213828130",
                        PhoneNumberConfirmed = true,
                        StatusId = status.Id,
                        TimeZoneId = timeZone.Id
                    };
                    if (!context.Users.Any(u => u.Email == customer.Email))
                    {
                        var result = _userManager.CreateAsync(customer, "ItSolution1@345").Result;
                        
                        var ro = _userManager.AddToRoleAsync(customer, UserRoles.Customer.ToString()).Result;

                        var city = context.Cities.FirstOrDefault(c => c.Name.Equals("Karachi"));
                        if (city == null)
                        {
                            throw new Exception($"{nameof(city)} is not found, seeds are malfunctioned.");
                        }
                        var address = new Addresses
                        {
                            UserId = customer.Id,
                            Address = "Shahrah e faisal",
                            CountryId = country.Id,
                            CityId = city.Id,
                            IsDefault = true
                        };
                        var addr = context.Addresses.AddAsync(address).Result;
                        context.SaveChanges();
                    }

                    var compny = context.Companies.FirstOrDefault(c => c.Name.Equals("KFC"));
                    if (compny == null)
                    {
                        throw new Exception($"{nameof(compny)} is not found, seeds are malfunctioned.");
                    }
                    var agency = new ApplicationUser
                    {
                        Email = "itsolution.agency@yopmail.com",
                        UserName = "itsolution.agency@yopmail.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "itsolution.agency@yopmail.com",
                        NormalizedUserName = "itsolution.agency@yopmail.com",
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        TwoFactorTypeId = twoFactorType.Id,
                        PhoneNumber = "03213828130",
                        PhoneNumberConfirmed = true,
                        StatusId = status.Id,
                        TimeZoneId = timeZone.Id
                    };
                    if (!context.Users.Any(u => u.Email == agency.Email))
                    {
                        var result = _userManager.CreateAsync(agency, "ItSolution1@345").Result;
                        
                        var ro = _userManager.AddToRoleAsync(agency, UserRoles.Agency.ToString()).Result;
                        var city = context.Cities.FirstOrDefault(c => c.Name.Equals("Peshawar"));
                        if (city == null)
                        {
                            throw new Exception($"{nameof(city)} is not found, seeds are malfunctioned.");
                        }
                        var address = new Addresses
                        {
                            UserId = agency.Id,
                            Address = "Saddar",
                            CountryId = country.Id,
                            CityId = city.Id,
                            IsDefault = true
                        };
                        var addr = context.Addresses.AddAsync(address).Result;
                        context.SaveChanges();
                    }
                }

                #endregion Users Roles UserRoles TwoFactorTypes


                #region NotificationTemplates NotificationTypes

                var notificationTypes = new List<NotificationType>()
                {
                    new NotificationType() { Id = NotificationTypes.Email, Name = NotificationName.Email },
                    new NotificationType() { Id = NotificationTypes.Sms, Name = NotificationName.Sms },
                    new NotificationType() { Id = NotificationTypes.Site, Name = NotificationName.Site },
                    new NotificationType() { Id = NotificationTypes.Push, Name = NotificationName.Push }
                };
                var newNotificationTypes = notificationTypes.Where(n => !context.NotificationTypes.Select(no => no.Id).Contains(n.Id));
                if (newNotificationTypes.Any())
                {
                    context.NotificationTypes.AddRange(newNotificationTypes);
                    context.SaveChanges();
                }

                var notificationTemplates = new List<NotificationTemplate>()
                {
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.EmailUserRegisteration,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Email confirmation when user is registered.",
                        Subject = "ItSolution Account Activation",
                        MessageBody = "Hi #Name </br></br>"
                                        + "Thank you for registering in ItSolution. "
                                        + "Click <a href=\"#Link\">here</a> to activate your account."
                        // Todo
                        //MessageBody = Resources.email_template.Replace("#Greeting", "Hola #Name")
                        //                                      .Replace("#Content", "¿Cómo estás? #Sender te hizo una transferencia. Revisa tu Actividad en dale! para ver los detalles de la transacción. ")
                        //                                      .Replace("#Closing", $"Nos vemos en {Resources.system_name}.")
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.SmsUserRegisteration,
                        NotificationTypeId = NotificationTypes.Sms,
                        Description = "Confirmation sms when user is registered.",
                        Subject = string.Empty,
                        MessageBody = "Todo"
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.EmailForgotPassword,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Email, when user click on forget password.",
                        Subject = "ItSolution Reset Password",
                        MessageBody = "Hi #Name </br></br>"
                                    + "Please click <a href=\"#Link\">here</a> to reset your password."
                        //Todo
                        //MessageBody = Resources.email_template.Replace("#Greeting", "Hola #Name")
                        //                                      .Replace("#Content", "¿Cómo estás? #Sender te hizo una transferencia. Revisa tu Actividad en dale! para ver los detalles de la transacción. ")
                        //                                      .Replace("#Closing", $"Nos vemos en {Resources.system_name}.")
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.SmsForgotPassword,
                        NotificationTypeId = NotificationTypes.Sms,
                        Description = "Code sent in sms, for forget password.",
                        Subject = string.Empty,
                        MessageBody = "Todo"
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.EmailSetPassword,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Confirmation email when an external user set password.",
                        Subject = "ItSolution Account Activation",
                        MessageBody = "Hi #Name </br></br>"
                                    + "Thank you for adding a local account in ItSolution. "
                                    + "Click <a href=\"#Link\">here</a> to activate your account"
                        // Todo
                        //MessageBody = Resources.email_template.Replace("#Greeting", "Hola #Name")
                        //                                      .Replace("#Content", "¿Cómo estás? #Sender te hizo una transferencia. Revisa tu Actividad en dale! para ver los detalles de la transacción. ")
                        //                                      .Replace("#Closing", $"Nos vemos en {Resources.system_name}.")
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.SmsSetPassword,
                        NotificationTypeId = NotificationTypes.Sms,
                        Description = "Confirmation sms when an external user set password.",
                        Subject = string.Empty,
                        MessageBody = "Todo"
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.EmailChangePassword,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Confirmation email when user change the password.",
                        Subject = "ItSolution Email Change",
                        MessageBody = "Hi #Name </br></br>"
                                    + "Click <a href=\"#Link\">here</a> to confirm your email to change it."
                                    + "</br></br> Thank you so much."
                        //Todo:
                        //MessageBody = Resources.email_template.Replace("#Greeting", "Hola #Name")
                        //                                      .Replace("#Content", "¿Cómo estás? #Sender te hizo una transferencia. Revisa tu Actividad en dale! para ver los detalles de la transacción. ")
                        //                                      .Replace("#Closing", $"Nos vemos en {Resources.system_name}.")
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.SmsChangePassword,
                        NotificationTypeId = NotificationTypes.Sms,
                        Description = "Confirmation sms when user change the password.",
                        Subject = string.Empty,
                        MessageBody = "Todo"
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.EmailTwoFactorToken,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Code in email for tow factor authentication.",
                        Subject = "ItSolution Code",
                        MessageBody = "Hi #Name </br></br>"
                                    + "#Token is your code."
                                    + "</br></br> Thank you so much."
                        //Todo:
                        //MessageBody = Resources.email_template.Replace("#Greeting", "Hola #Name")
                        //                                      .Replace("#Content", "¿Cómo estás? #Sender te hizo una transferencia. Revisa tu Actividad en dale! para ver los detalles de la transacción. ")
                        //                                      .Replace("#Closing", $"Nos vemos en {Resources.system_name}.")
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.SmsTwoFactorToken,
                        NotificationTypeId = NotificationTypes.Sms,
                        Description = "Code in sms for tow factor authentication.",
                        Subject = string.Empty,
                        MessageBody = "Todo"
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.EmailUserStatusChange,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Email to user whan user is block or status change to any status in application.",
                        Subject = "ItSolution Account Restricted",
                        MessageBody = "Todo"
                        //MessageBody = Resources.email_template.Replace("#Greeting", "Hola #Name")
                        //                                      .Replace("#Content", "¿Cómo estás? #Sender te hizo una transferencia. Revisa tu Actividad en dale! para ver los detalles de la transacción. ")
                        //                                      .Replace("#Closing", $"Nos vemos en {Resources.system_name}.")
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.SmsUserStatusChange,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Sms to user whan user is block or status change to any status in application.",
                        Subject = string.Empty,
                        MessageBody = "Todo"
                    },
                    new NotificationTemplate()
                    {
                        Id = NotificationTemplates.ConfirmClientAgency,
                        NotificationTypeId = NotificationTypes.Email,
                        Description = "Email Invitiation from Client.",
                        Subject = "Invitiation from Client #AgencyName",
                        MessageBody = "Dear #Name </br></br>"
                                        + "You are invited to join client #ClientName (#ClientEmail). "
                                        + "Please click <a href=\"#Link\">here</a> to accept the invitation.<br/>."
                                        + "<br /><br />Thanks"
                    },
                };
                var newNotificationTemplates = notificationTemplates.Where(n => !context.NotificationTemplates.Select(nt => nt.Id).Contains(n.Id));
                if (newNotificationTemplates.Any())
                {
                    context.NotificationTemplates.AddRange(newNotificationTemplates);
                    context.SaveChanges();
                }

                #endregion NotificationTemplates NotificationTypes
            }
        }
    }
}
