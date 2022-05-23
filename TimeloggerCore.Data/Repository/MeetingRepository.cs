using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TimeloggerCore.Data.Repository
{
    public class MeetingRepository : BaseRepository<Meeting, int>, IMeetingRepository
    {
        public MeetingRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<List<Meeting>> GetAllMeeting(string userId)
        {
            var meeting = DbContext.Meetings.Where(x => !x.IsDeleted && x.UserId == userId).Include(x => x.User);
            return await meeting.ToListAsync();
        }
    }
}
