using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TimeloggerCore.Common.Models;
using System.Linq.Expressions;
using TimeloggerCore.Common.Extensions;

namespace TimeloggerCore.Data.Repository
{
    public class TimeLogRepository : BaseRepository<TimeLog, int>, ITimeLogRepository
    {
        public TimeLogRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<TimeLog> GetActiveProject(string userId)
        {
                var timelog = await FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive,
                    null,
                    i => i.Project);
                return timelog;
            
        }

        public async Task<List<TimeLog>> GetActiveProjects(List<string> users)
        {
            var timelog = await GetAsync(x => x.IsActive && users.Contains(x.UserId),
                null,
                i => i.Project);
            return timelog;
        }

        public async Task<List<TimeLog>> GetLastWeekLogs()
        {
            var timelog = await GetAsync(x => !x.IsActive && x.LogDate > DateTime.UtcNow.AddDays(-6).Date,
                null,
                i => i.Project);
            return timelog;
        }

        public async Task<List<TimeLog>> GetLogs()
        {
            var timelog = await GetAsync(x => true,
                null,
                i => i.Project, i => i.User, i => i.WorkSessions, i => i.Project.Invitations);
            return timelog;
        }

        public async Task<List<TimeLog>> GetLogsForProject(int projectId)
        {
            var timelog = await GetAsync(x => x.ProjectID == projectId,
                null, 
                i => i.Project);
            return timelog;
        }

        public async Task<List<TimeLog>> GetLogs(string userId)
        {
            var timelog = await GetAsync(x => x.UserId == userId,
                null,
                i => i.Project);
            return timelog;
        }

        public async Task<List<TimeLog>> GetProgress(int day)
        {
            var date = DateTime.UtcNow;
            if (day == 1)
            {
                return await GetAsync(x => !x.IsActive && x.LogDate == date.Date,
                    null,
                    i => i.Project, i => i.User);
            }
            else if (day == 2)
            {
                date = date.AddDays(-(int)date.DayOfWeek).Date;
                return await GetAsync(x => !x.IsActive && x.LogDate > date,
                    null,
                    i => i.Project, i => i.User);
            }
            else return null;
        }

        public async Task<List<TimeLog>> GetProjectTimeLogs()
        {
           return await GetAsync(
               null, 
               null,
               i => i.Project, i => i.WorkSessions);
        }

        public async Task<List<TimeLog>> GetAllWorkerProjectTimeLogs(string[] workerIds, int Type)
        {
            DateTime currentDate = DateTime.Now;
            List<TimeLog> timeLogs = new List<TimeLog>();
            string[] Ids = workerIds.ToArray();
                if (Type == 1)
                {
                    DateTime previousWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek).Date;
                    
                    timeLogs = await GetAsync(x => Ids.Contains(x.UserId) && x.LogDate > previousWeek.Date);
                    timeLogs.ForEach(p =>
                    {
                        p.User = null;
                        p.WorkSessions = null;
                    });
                    //timeLogs = await (from p in context.TimeLog
                    //                  where Ids.Contains(p.UserId)
                    //                      && p.LogDate > previousWeek.Date
                    //                  select new TimeLogViewModel
                    //                  {
                    //                      Description = p.Description,
                    //                      Id = p.Id,
                    //                      IsActive = p.IsActive,
                    //                      LogDate = p.LogDate,
                    //                      ProjectId = p.ProjectID,
                    //                      StartTime = p.StartTime,
                    //                      StopTime = p.StopTime,
                    //                      TrackType = p.TrackType,
                    //                      UserId = p.UserId,
                    //                      Project = new ProjectViewModel(),
                    //                      User=new ApplicationUser(),
                    //                      WorkSessions = new List<WorkSessionViewModel>()

                    //                  }).ToListAsync();
                }
                else if (Type == 2)
                {
                    timeLogs = await GetAsync(x => Ids.Contains(x.UserId) && x.LogDate.Month == currentDate.Month);
                    timeLogs.ForEach(p =>
                    {
                        p.User = null;
                        p.WorkSessions = null;
                    });

                }
                else if (Type == 3)
                {
                    timeLogs.ForEach(p =>
                    {
                        p.User = null;
                        p.WorkSessions = null;
                    });
                }
            return timeLogs;
        }

        public async Task<List<TimeLog>> GetReport(TimeSheetModel model)
        {
            return await GetAsync(x => x.LogDate >= model.FromDate.Date && x.LogDate <= model.ToDate.Date && x.StopTime.HasValue, 
                null,
                i => i.Project, i => i.User, i => i.WorkSessions);
        }

        public async Task<List<TimeLog>> GetTimelogReport(Expression<Func<TimeLog, bool>> dateSelect, Expression<Func<TimeLog, bool>> userSelect, Expression<Func<TimeLog, bool>> projectSelect)
        {
            var exp = PredicateBuilder.And(dateSelect, userSelect);
            exp = PredicateBuilder.And(exp, projectSelect);
            var timelogs = await GetAsync(
                exp,
                null,
                i => i.Project, i => i.User, i => i.WorkSessions);
            return timelogs;
        }
        public async Task<List<TimeLog>> GetWorkSession(Expression<Func<TimeLog, bool>> workSession)
        {
            var timelogs = await GetAsync(workSession);
            return timelogs;
        }
        public async Task<List<TimeLog>> GetToday()
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            return await GetAsync(x => x.LogDate == date.Date,
                null,
                i => i.Project, i => i.User, i => i.WorkSessions, i => i.Project.Invitations);
        }

        public async Task<List<TimeLog>> GetTodayTimeLogs(int[] projectIds, string[] workerIds)
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            return await GetAsync(x => (workerIds).Contains(x.UserId) && (projectIds).Contains(x.ProjectID) && x.LogDate == date.Date,
                null, 
                i => i.Project, i => i.User, i => i.WorkSessions, i => i.Project.Invitations);
        }
        public async Task<List<TimeLog>> GetTeamWorkerTime(string teamleadId)
        {
            List<Invitation> teamleadWorker = new List<Invitation>();
            teamleadWorker = await DbContext.Invitations.Where(x => x.ClientID == teamleadId && x.IsAccepted && x.ExistingUser).ToListAsync();
            string[] userIds = teamleadWorker.Select(x => x.UserID).Distinct().ToArray();
            var timelog = await GetAsync(x => userIds.Contains(x.UserId) && !x.IsActive && x.StopTime != null);
            return timelog;
        }
    }
}
