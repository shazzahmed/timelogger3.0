using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Data.Repository
{
    public class StatusTypeRepository : BaseRepository<StatusType, int>, IStatusTypeRepository
    {
        public StatusTypeRepository(ISqlServerDbContext context) : base(context)
        {
        }
    }
}
