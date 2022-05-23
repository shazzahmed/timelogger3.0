using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IMeetingRepository : IBaseRepository<Meeting, int>
    {
        Task<List<Meeting>> GetAllMeeting(string userId);
    }
}
