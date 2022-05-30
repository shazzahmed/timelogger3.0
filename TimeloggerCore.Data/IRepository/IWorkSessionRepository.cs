using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IWorkSessionRepository : IBaseRepository<WorkSession, int>
    {
        //List<WorkSession> InsertList(List<WorkSession> lstworkSessions);
        Task<List<WorkSession>> GetWorkSessions(DateTime date);
        Task<WorkSession> GetLastWorkSession(int logId);
        void DeleteSessions(List<WorkSession> sessions);
        Task<List<WorkSession>> WorkSessionsList(int[] timelogIds);
    }
}
