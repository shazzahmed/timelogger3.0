using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Data.Repository
{
    public class FeedbackRepository : BaseRepository<Feedback, int>, IFeedbackRepository
    {
        public FeedbackRepository(ISqlServerDbContext context) : base(context)
        {
        }
    }
}
