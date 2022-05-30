using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TimeloggerCore.Services
{
    public class TimeLogService : BaseService<TimeLogModel, TimeLog, int>, ITimeLogService
    {
        private readonly ITimeLogRepository _timeLogRepository;

        public TimeLogService(IMapper mapper, ITimeLogRepository timeLogRepository, IUnitOfWork unitOfWork) : base(mapper, timeLogRepository, unitOfWork)
        {
            _timeLogRepository = timeLogRepository;
        }
        public Task<TimeLogModel> AddTimelog(TimeLogModel timeLogModel)
        {
            return Add(timeLogModel);
        }

        public Task<TimeLog> GetActiveProject(string userId)
        {
            return _timeLogRepository.GetActiveProject(userId);
        }

        public Task<List<TimeLog>> GetActiveProjects(List<string> users)
        {
            return _timeLogRepository.GetActiveProjects(users);
        }

        public Task<List<TimeLog>> GetAllWorkerProjectTimeLogs(string[] workerIds, int Type)
        {
            return _timeLogRepository.GetAllWorkerProjectTimeLogs(workerIds, Type);
        }

        public Task<List<TimeLog>> GetLastWeekLogs()
        {
            return _timeLogRepository.GetLastWeekLogs();
        }

        public Task<List<TimeLog>> GetLogs()
        {
            return _timeLogRepository.GetLogs();
        }

        public Task<List<TimeLog>> GetLogs(string userId)
        {
            return _timeLogRepository.GetLogs(userId);
        }

        public Task<List<TimeLog>> GetLogsForProject(int projectId)
        {
            return _timeLogRepository.GetLogsForProject(projectId);
        }

        public Task<List<TimeLog>> GetProgress(int day)
        {
            return _timeLogRepository.GetProgress(day);
        }

        public Task<List<TimeLog>> GetProjectTimeLogs()
        {
            return _timeLogRepository.GetProjectTimeLogs();
        }

        public Task<List<TimeLog>> GetReport(TimeSheetModel model)
        {
            return _timeLogRepository.GetReport(model);
        }

        public Task<List<TimeLog>> GetTeamWorkerTime(string teamleadId)
        {
            return _timeLogRepository.GetTeamWorkerTime(teamleadId);
        }

        public Task<List<TimeLog>> GetTimelogReport(Expression<Func<TimeLog, bool>> dateSelect, Expression<Func<TimeLog, bool>> userSelect, Expression<Func<TimeLog, bool>> projectSelect)
        {
            return _timeLogRepository.GetTimelogReport(dateSelect, userSelect, projectSelect);
        }

        public Task<List<TimeLog>> GetToday()
        {
            return _timeLogRepository.GetToday();
        }

        public Task<List<TimeLog>> GetTodayTimeLogs(int[] projectIds, string[] workerIds)
        {
            return _timeLogRepository.GetTodayTimeLogs(projectIds, workerIds);
        }

        public Task<List<TimeLog>> GetWorkSession(Expression<Func<TimeLog, bool>> workSession)
        {
            return _timeLogRepository.GetWorkSession(workSession);
        }
    }
}
