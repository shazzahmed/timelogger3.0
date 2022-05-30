using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.Repository
{
    public class WorkSessionRepository : BaseRepository<WorkSession, int>, IWorkSessionRepository
    {
        public WorkSessionRepository(ISqlServerDbContext context) : base(context)
        {
        }
        //public async List<WorkSession> InsertList(List<WorkSession> lstworkSessions)
        //{

        //    var aa =await AddAsync(lstworkSessions);
        //    return lstworkSessions;

        //}

        public Task<List<WorkSession>> GetWorkSessions(DateTime date)
        {
            return GetAsync(w => w.StartTime.Date == date.Date);
        }

        public async Task<WorkSession> GetLastWorkSession(int logId)
        {
            return await FirstOrDefaultAsync(x => x.TimeLogId == logId, x=> x.OrderByDescending(x=> x.Id));
        }

        public void DeleteSessions(List<WorkSession> sessions)
        {
            DbContext.Entry(sessions).State = EntityState.Modified;
        }
        public async Task<List<WorkSession>> WorkSessionsList(int[] timelogIds)
        {
                int[] ids = timelogIds.ToArray();
            var workSessions = await GetAsync(x => ids.Contains(x.TimeLogId), null, x => x.TimeLog.Project);
            if (workSessions.Count() != 0)
                workSessions.ForEach(x =>
                {
                    if (x.TimeLog != null)
                        x.TimeLog.WorkSessions = null;
                });
            return workSessions;
        }
    }
}
