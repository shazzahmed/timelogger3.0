using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Data.Entities;

namespace TimeloggerCore.Data.Mapping
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Addresses, AddressModel>().ReverseMap();
            CreateMap<City, CityModel>().ReverseMap();
            CreateMap<ClientAgency, ClientAgencyModel>().ReverseMap();
            CreateMap<ClientWorker, ClientWorkerModel>().ReverseMap();
            CreateMap<Company, CompanyModel>().ReverseMap();
            CreateMap<Country, CountryModel>().ReverseMap();
            CreateMap<CountryCode, CountryCodeModel>().ReverseMap();
            CreateMap<Feedback, FeedbackModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserModel>().ReverseMap();
            CreateMap<Invitation, InvitationModel>().ReverseMap();
            CreateMap<InvitationRequest, InvitationRequestModel>().ReverseMap();
            CreateMap<Meeting, MeetingModel>().ReverseMap();
        }
    }
}
