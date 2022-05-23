using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TimeloggerCore.Data.Repository
{
    public class WorkSessionRepository : BaseRepository<WorkSession, int>, IWorkSessionRepository
    {
        public WorkSessionRepository(ISqlServerDbContext context) : base(context)
        {
        }
        public List<WorkSession> InsertList(List<WorkSession> lstworkSessions)
        {

            Add(lstworkSessions);
            return lstworkSessions;

        }

        public List<WorkSession> GetWorkSessions(DateTime date)
        {
            return DbContext.WorkSession.ToList().Where(w => w.StartTime.Date == date.Date).ToList();
        }

        public WorkSession GetLastWorkSession(int logId)
        {
            return DbContext.WorkSession.Where(w => w.TimeLogId == logId).OrderByDescending(w => w.Id).ToList().LastOrDefault();
        }

        public void DeleteSessions(List<WorkSession> sessions)
        {
            DbContext.Entry(sessions).State = EntityState.Modified;
        }
        public List<WorkSession> WorkSessionsList(int[] timelogIds)
        {
            List<WorkSession> workSessions = new List<WorkSession>();
            try
            {
                int[] ids = timelogIds.ToArray();
                workSessions = DbContext.WorkSession.Include(x => x.TimeLog.Project)
                .Where(x => ids.Contains(x.TimeLogId)).ToList();
                if (workSessions.Count() != 0)
                    workSessions.ForEach(x => {
                        if (x.TimeLog != null)
                            x.TimeLog.WorkSessions = null;
                    });
            }
            catch (Exception ex)
            {

            }
            return workSessions;
        }
    }
}
