using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Data.IRepository
{
    public interface IProjectWorkersRepository : IBaseRepository<ProjectWorkers, int>
    {
        Task<List<ProjectWorkersModel>> GetProjectWorkers(int ProjectId);
    }
}
