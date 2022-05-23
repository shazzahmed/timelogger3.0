using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using TimeZone = TimeloggerCore.Data.Entities.TimeZone;

namespace TimeloggerCore.Data.Repository
{
    public class TimeZoneRepository : BaseRepository<TimeZone, int>, ITimeZoneRepository
    {
        public TimeZoneRepository(ISqlServerDbContext context) : base(context)
        {
        }
    }
}
