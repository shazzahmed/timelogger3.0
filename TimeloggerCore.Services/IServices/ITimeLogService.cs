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
        Task<List<TimeLog>> GetLastWeekLogs();
        Task<List<TimeLog>> GetProjectTimeLogs();
        Task<List<TimeLog>> GetAllWorkerProjectTimeLogs(string[] workerIds, int Type);
        Task<TimeLog> GetActiveProject(string userId);
        Task<List<TimeLog>> GetProgress(int day);
        Task<List<TimeLog>> GetActiveProjects(List<string> users);
        Task<List<TimeLog>> GetToday();
        Task<List<TimeLog>> GetTodayTimeLogs(int[] projectIds, string[] workerIds);
        Task<List<TimeLog>> GetLogs();
        Task<List<TimeLog>> GetLogsForProject(int projectId);
        Task<List<TimeLog>> GetLogs(string userId);
        Task<List<TimeLog>> GetReport(TimeSheetModel model);
        Task<List<TimeLog>> GetTimelogReport(Expression<Func<TimeLog, bool>> dateSelect, Expression<Func<TimeLog, bool>> userSelect, Expression<Func<TimeLog, bool>> projectSelect);
        Task<List<TimeLog>> GetWorkSession(Expression<Func<TimeLog, bool>> workSession);
        Task<List<TimeLog>> GetTeamWorkerTime(string teamleadId);
        Task<TimeLogModel> AddTimelog(TimeLogModel timeLogModel);
    }
}
