using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IPreviousPasswordsRepository : IBaseRepository<PreviousPassword, int>
    {
    }
}
