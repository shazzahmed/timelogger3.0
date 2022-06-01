using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TimeloggerCore.Services.IService
{
    public interface ITimeLogService : IBaseService<TimeLogModel, TimeLog, int>
    {
        Task<BaseModel> GetLastWeekLogs();
        Task<BaseModel> GetProjectTimeLogs();
        Task<BaseModel> GetAllWorkerProjectTimeLogs(string[] workerIds, int Type);
        Task<BaseModel> GetActiveProject(string userId);
        Task<BaseModel> GetProgress(int day);
        Task<BaseModel> GetActiveProjects(List<string> users);
        Task<BaseModel> GetToday();
        Task<BaseModel> GetTodayTimeLogs(int[] projectIds, string[] workerIds);
        Task<BaseModel> GetLogs();
        Task<BaseModel> GetLogsForProject(int projectId);
        Task<BaseModel> GetLogs(string userId);
        Task<BaseModel> GetReport(TimeSheetModel model);
        Task<BaseModel> GetTimelogReport(Expression<Func<TimeLog, bool>> dateSelect, Expression<Func<TimeLog, bool>> userSelect, Expression<Func<TimeLog, bool>> projectSelect);
        Task<BaseModel> GetWorkSession(Expression<Func<TimeLog, bool>> workSession);
        Task<BaseModel> GetTeamWorkerTime(string teamleadId);
        Task<BaseModel> AddTimelog(TimeLogModel timeLogModel);
    }
}
