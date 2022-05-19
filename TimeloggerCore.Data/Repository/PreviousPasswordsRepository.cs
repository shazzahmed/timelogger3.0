using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using System.Linq;
using TimeloggerCore.Common.Options;
using System.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace TimeloggerCore.Data.Repository
{
    public class PreviousPasswordsRepository : BaseRepository<PreviousPassword, int>, IPreviousPasswordsRepository
    {
        ////private readonly SecurityOptions _securityOptions;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly ISqlServerDbContext _dbContext;
        public PreviousPasswordsRepository(
            ISqlServerDbContext sqlServerDbContext,
            //SecurityOptions securityOptions,
            UserManager<ApplicationUser> userManager
            ) : base(sqlServerDbContext)
        {
            //_securityOptions = securityOptions;
            _userManager = userManager;
            _dbContext = sqlServerDbContext;
        }
    }
}
