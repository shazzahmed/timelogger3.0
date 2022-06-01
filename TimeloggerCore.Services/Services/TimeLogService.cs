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

        public TimeLogService(
            IMapper mapper, 
            ITimeLogRepository timeLogRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, timeLogRepository, unitOfWork)
        {
            _timeLogRepository = timeLogRepository;
        }
        public async Task<BaseModel> AddTimelog(TimeLogModel timeLogModel)
        {
            var result = await Add(timeLogModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<TimeLogModel, TimeLog>(result)
            };
        }

        public async Task<BaseModel> GetActiveProject(string userId)
        {
            var result = await _timeLogRepository.GetActiveProject(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<TimeLog, TimeLogModel>(result)
            };
        }

        public async Task<BaseModel> GetActiveProjects(List<string> users)
        {
            var result = await _timeLogRepository.GetActiveProjects(users);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
            
        }

        public async Task<BaseModel> GetAllWorkerProjectTimeLogs(string[] workerIds, int Type)
        {
            var result = await _timeLogRepository.GetAllWorkerProjectTimeLogs(workerIds, Type);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetLastWeekLogs()
        {
            var result = await _timeLogRepository.GetLastWeekLogs();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetLogs()
        {
            var result = await _timeLogRepository.GetLogs();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetLogs(string userId)
        {
            var result = await _timeLogRepository.GetLogs(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetLogsForProject(int projectId)
        {
            var result = await _timeLogRepository.GetLogsForProject(projectId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetProgress(int day)
        {
            var result = await _timeLogRepository.GetProgress(day);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetProjectTimeLogs()
        {
            var result = await _timeLogRepository.GetProjectTimeLogs();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetReport(TimeSheetModel model)
        {
            var result = await _timeLogRepository.GetReport(model);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetTeamWorkerTime(string teamleadId)
        {
            var result = await _timeLogRepository.GetTeamWorkerTime(teamleadId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetTimelogReport(Expression<Func<TimeLog, bool>> dateSelect, Expression<Func<TimeLog, bool>> userSelect, Expression<Func<TimeLog, bool>> projectSelect)
        {
            var result = await _timeLogRepository.GetTimelogReport(dateSelect, userSelect, projectSelect);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetToday()
        {
            var result = await _timeLogRepository.GetToday();
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetTodayTimeLogs(int[] projectIds, string[] workerIds)
        {
            var result = await _timeLogRepository.GetTodayTimeLogs(projectIds, workerIds);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }

        public async Task<BaseModel> GetWorkSession(Expression<Func<TimeLog, bool>> workSession)
        {
            var result = await _timeLogRepository.GetWorkSession(workSession);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<TimeLog>, List<TimeLogModel>>(result)
            };
        }
    }
}
