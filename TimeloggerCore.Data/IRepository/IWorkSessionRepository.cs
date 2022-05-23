using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Data.IRepository
{
    public interface IWorkSessionRepository : IBaseRepository<WorkSession, int>
    {
        List<WorkSession> InsertList(List<WorkSession> lstworkSessions);
        List<WorkSession> GetWorkSessions(DateTime date);
        WorkSession GetLastWorkSession(int logId);
        void DeleteSessions(List<WorkSession> sessions);
        List<WorkSession> WorkSessionsList(int[] timelogIds);
    }
}
