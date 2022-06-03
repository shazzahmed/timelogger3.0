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
    public class WorkSessionService : BaseService<WorkSessionModel, WorkSession, int>, IWorkSessionService
    {
        private readonly IWorkSessionRepository _workSessionRepository;

        public WorkSessionService(
            IMapper mapper, 
            IWorkSessionRepository workSessionRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, workSessionRepository, unitOfWork)
        {
            _workSessionRepository = workSessionRepository;
        }
        public void DeleteSessions(List<WorkSession> sessions)
        {
            _workSessionRepository.DeleteSessions(sessions);
        }
        
        public async Task<BaseModel> GetLastWorkSession(int logId)
        {
            var result = await _workSessionRepository.GetLastWorkSession(logId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<WorkSession, WorkSessionModel>(result)
            };
        }
        public async Task<BaseModel> GetWorkSessions(DateTime date)
        {
            var result = await _workSessionRepository.GetWorkSessions(date);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<WorkSession>, List<WorkSessionModel>>(result)
            };
        }
        public async Task<BaseModel> InsertList(List<WorkSessionModel> lstworkSessions)
        {
            var result = await AddRange(lstworkSessions);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<WorkSessionModel>, List<WorkSession>>(result)
            };
        }
        public async Task<BaseModel> WorkSessionsList(int[] timelogIds)
        {
            var result = await _workSessionRepository.WorkSessionsList(timelogIds);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<WorkSession>, List<WorkSessionModel>>(result)
            };
        }
    }
}
