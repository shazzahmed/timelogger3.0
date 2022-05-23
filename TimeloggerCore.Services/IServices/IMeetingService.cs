using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IMeetingService : IBaseService<MeetingModel, Meeting, int>
    {
        Task<List<Meeting>> GetAllMeeting(string userId);
    }
}
