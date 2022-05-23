using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services
{
    public class WorkerService : BaseService<ProjectWorkersModel, ProjectWorkers, int>, IWorkerService
    {
        private readonly IProjectWorkersRepository _projectWorkersRepository;

        public WorkerService(IMapper mapper, IProjectWorkersRepository projectWorkersRepository, IUnitOfWork unitOfWork) : base(mapper, projectWorkersRepository, unitOfWork)
        {
            _projectWorkersRepository = projectWorkersRepository;
        }
        public Task<List<ProjectWorkersModel>> GetProjectWorkers(int ProjectId)
        {
            return _projectWorkersRepository.GetProjectWorkers(ProjectId);
        }
    }
}
