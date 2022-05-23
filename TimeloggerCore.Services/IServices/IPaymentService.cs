using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;

namespace TimeloggerCore.Services.IService
{
    public interface IPaymentService : IBaseService<PaymentModel, Payment, int>
    {
        Task<int> GetUserByEmail(InvitationRequest invitationRequest);
        Task<ApplicationUser> UserInfo(string userId);
    }
}
