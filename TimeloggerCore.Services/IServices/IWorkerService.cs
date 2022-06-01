using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IWorkerService : IBaseService<ProjectWorkersModel, ProjectWorkers, int>
    {
        Task<BaseModel> GetProjectWorkers(int ProjectId);
    }
}
