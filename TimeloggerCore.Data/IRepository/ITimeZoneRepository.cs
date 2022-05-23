using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeZone = TimeloggerCore.Data.Entities.TimeZone;

namespace TimeloggerCore.Data.IRepository
{
    public interface ITimeZoneRepository : IBaseRepository<TimeZone, int>
    {
    }
}
