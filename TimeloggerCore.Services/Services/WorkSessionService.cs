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

        public WorkSessionService(IMapper mapper, IWorkSessionRepository workSessionRepository, IUnitOfWork unitOfWork) : base(mapper, workSessionRepository, unitOfWork)
        {
            _workSessionRepository = workSessionRepository;
        }
        public void DeleteSessions(List<WorkSession> sessions)
        {
            _workSessionRepository.DeleteSessions(sessions);
        }

        public Task<WorkSession> GetLastWorkSession(int logId)
        {
            return _workSessionRepository.GetLastWorkSession(logId);
        }

        public Task<List<WorkSession>> GetWorkSessions(DateTime date)
        {
            return _workSessionRepository.GetWorkSessions(date);
        }

        public async Task<List<WorkSessionModel>> InsertList(List<WorkSessionModel> lstworkSessions)
        {
            return await AddRange(lstworkSessions);
            //return _workSessionRepository.InsertList(lstworkSessions);
        }

        public Task<List<WorkSession>> WorkSessionsList(int[] timelogIds)
        {
            return _workSessionRepository.WorkSessionsList(timelogIds);
        }
    }
}
