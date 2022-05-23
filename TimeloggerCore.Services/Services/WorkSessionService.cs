using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

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

        public WorkSession GetLastWorkSession(int logId)
        {
            return _workSessionRepository.GetLastWorkSession(logId);
        }

        public List<WorkSession> GetWorkSessions(DateTime date)
        {
            return _workSessionRepository.GetWorkSessions(date);
        }

        public List<WorkSession> InsertList(List<WorkSession> lstworkSessions)
        {
            return _workSessionRepository.InsertList(lstworkSessions);
        }

        public List<WorkSession> WorkSessionsList(int[] timelogIds)
        {
            return _workSessionRepository.WorkSessionsList(timelogIds);
        }
    }
}
