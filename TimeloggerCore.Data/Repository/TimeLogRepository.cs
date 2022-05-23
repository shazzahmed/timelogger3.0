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

namespace TimeloggerCore.Data.Repository
{
    public class TimeLogRepository : BaseRepository<TimeLog, int>, ITimeLogRepository
    {
        public TimeLogRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public async Task<TimeLog> GetActiveProject(string userId)
        {
            try
            {
                var timelog = await DbContext.TimeLog.Include("Project").SingleOrDefaultAsync(t => t.UserId == userId && t.IsActive);
                return timelog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TimeLog>> GetActiveProjects(List<string> users)
        {
            return await DbContext.TimeLog.Include(x => x.Project).Where(t => t.IsActive && users.Contains(t.UserId)).ToListAsync();
        }

        public async Task<List<TimeLog>> GetLastWeekLogs()
        {
            var now = DateTime.UtcNow;
            var a = now.AddDays(-6).Date;
            return await DbContext.TimeLog.Include("Project").Where(t => !t.IsActive && t.LogDate > a).ToListAsync();
        }

        public async Task<List<TimeLog>> GetLogs()
        {
            return await DbContext.TimeLog.Include(t => t.Project).Include(t => t.User).Include(t => t.WorkSessions).Include(t => t.Project.Invitations).Where(t => true).ToListAsync();
        }

        public async Task<List<TimeLog>> GetLogsForProject(int projectId)
        {
            return await DbContext.TimeLog.Include("Project").Where(t => t.ProjectID == projectId).ToListAsync();
        }

        public async Task<List<TimeLog>> GetLogs(string userId)
        {
            return await DbContext.TimeLog.Include("Project").Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<List<TimeLog>> GetProgress(int day)
        {
            var now = DateTime.UtcNow;
            if (day == 1)
            {
                return await DbContext.TimeLog.Include("Project").Include("User").Where(t => !t.IsActive && t.LogDate == now.Date).ToListAsync();
            }
            else if (day == 2)
            {
                var date = now.AddDays(-(int)now.DayOfWeek).Date;
                return await DbContext.TimeLog.Include("Project").Include("User").Where(t => !t.IsActive && t.LogDate > date).ToListAsync();
            }
            else return null;
        }

        public async Task<List<TimeLog>> GetProjectTimeLogs() => await DbContext.TimeLog.Include(x => x.Project).Include(x => x.WorkSessions).ToListAsync();

        public async Task<List<TimeLog>> GetAllWorkerProjectTimeLogs(string[] workerIds, int Type)
        {
            DateTime currentDate = DateTime.Now;
            List<TimeLog> timeLogs = new List<TimeLog>();
            string[] Ids = workerIds.ToArray();
            try
            {
                if (Type == 1)
                {
                    DateTime previousWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek).Date;
                    timeLogs = await DbContext.TimeLog.Where(x => Ids.Contains(x.UserId)

                             && x.LogDate > previousWeek.Date).ToListAsync();
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
                    timeLogs = await DbContext.TimeLog.Where(x => Ids.Contains(x.UserId)

                && x.LogDate.Month == currentDate.Month
               ).ToListAsync();
                    timeLogs.ForEach(p =>
                    {
                        p.User = null;
                        p.WorkSessions = null;
                    });

                }
                else if (Type == 3)
                {
                    timeLogs = await DbContext.TimeLog.Where(x => Ids.Contains(x.UserId)

                    && x.LogDate.Year == currentDate.Year
                 ).ToListAsync();
                    timeLogs.ForEach(p =>
                    {
                        p.User = null;
                        p.WorkSessions = null;
                    });
                }

            }
            catch (Exception ex)
            {

            }
            return timeLogs;
        }

        public async Task<List<TimeLog>> GetReport(TimeSheetModel model)
        {


            return await DbContext.TimeLog
                .Include(t => t.Project)
                .Include(t => t.User)
                .Include(t => t.WorkSessions)
                .Where(t => t.LogDate >= model.FromDate.Date && t.LogDate <= model.ToDate.Date && t.StopTime.HasValue).ToListAsync();
        }

        public async Task<List<TimeLog>> GetTimelogReport(Expression<Func<TimeLog, bool>> dateSelect, Expression<Func<TimeLog, bool>> userSelect, Expression<Func<TimeLog, bool>> projectSelect)
        {
            var timelogs = await DbContext.TimeLog
           .Include(t => t.Project)
           .Include(t => t.User)
           .Include(t => t.WorkSessions)
           .Where(dateSelect).Where(userSelect).Where(projectSelect).ToListAsync();
            return timelogs;
        }
        public async Task<List<TimeLog>> GetWorkSession(Expression<Func<TimeLog, bool>> workSession)
        {
            var timelogs = await DbContext.TimeLog
                .Where(workSession).ToListAsync();
            return timelogs;
        }
        public async Task<List<TimeLog>> GetToday()
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            return await DbContext.TimeLog.Include(t => t.Project).Include(t => t.User).Include(t => t.WorkSessions).Include(t => t.Project.Invitations).Where(t => t.LogDate == date.Date).ToListAsync();
        }

        public async Task<List<TimeLog>> GetTodayTimeLogs(int[] projectIds, string[] workerIds)
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            return await DbContext.TimeLog.Where(t => (workerIds).Contains(t.UserId) && (projectIds).Contains(t.ProjectID)).Include(t => t.Project).Include(t => t.User).Include(t => t.WorkSessions).Include(t => t.Project.Invitations).Where(t => t.LogDate == date.Date).ToListAsync();
        }
        public async Task<List<TimeLog>> GetTeamWorkerTime(string teamleadId)
        {
            List<Invitation> teamleadWorker = new List<Invitation>();
            teamleadWorker = await DbContext.Invitations.Where(x => x.ClientID == teamleadId && x.IsAccepted && x.ExistingUser).ToListAsync();
            string[] userIds = teamleadWorker.Select(x => x.UserID).Distinct().ToArray();
            var timelog = await DbContext.TimeLog.Where(x => userIds.Contains(x.UserId) && !x.IsActive && x.StopTime != null).ToListAsync();
            return timelog;
        }

        public Task<TimeLog> AddTimelog(TimeLog timeLog)
        {
            var result = AddAsync(timeLog);
            DbContext.SaveChanges();
            return result;
        }
    }
}
