using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services
{
    public class MeetingService : BaseService<MeetingModel, Meeting, int>, IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMapper mapper, IMeetingRepository meetingRepository, IUnitOfWork unitOfWork) : base(mapper, meetingRepository, unitOfWork)
        {
            _meetingRepository = meetingRepository;
        }
        public Task<List<Meeting>> GetAllMeeting(string userId)
        {
            return _meetingRepository.GetAllMeeting(userId);
        }
    }
}
