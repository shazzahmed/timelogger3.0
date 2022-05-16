﻿using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Data.IRepository
{
    public interface IUserRepository : IBaseRepository<ApplicationUser, string>
    {
    }
}
