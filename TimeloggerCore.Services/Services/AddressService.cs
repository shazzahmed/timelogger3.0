using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services
{
    public class AddressService : BaseService<AddressModel, Addresses, int>, IAddressService
    {
        private readonly IAddressRepository addressRepository;

        public AddressService(IMapper mapper, IAddressRepository addressRepository, IUnitOfWork unitOfWork) : base(mapper, addressRepository, unitOfWork)
        {
            this.addressRepository = addressRepository;
        }
    }
}
