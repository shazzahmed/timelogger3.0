using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using ApplicationUser = TimeloggerCore.Data.Entities.ApplicationUser;
using TimeloggerCore.Core.ISecurity;

namespace TimeloggerCore.Services
{
    public class PaymentService : BaseService<PaymentModel, Payment, int>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, ISecurityService securityService, IUnitOfWork unitOfWork) : base(mapper, paymentRepository, unitOfWork)
        {
            _paymentRepository = paymentRepository;
        }
    }
}
