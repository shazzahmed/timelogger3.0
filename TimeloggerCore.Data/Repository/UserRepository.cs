using TimeloggerCore.Data.Database;
using TimeloggerCore.Core.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Data.Entities;

namespace TimeloggerCore.Data.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser, string>, IUserRepository
    {
        public UserRepository(ISqlServerDbContext context) : base(context)
        {
        }
    }
}
