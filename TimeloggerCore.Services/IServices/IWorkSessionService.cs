using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.IService
{
    public interface IWorkSessionService : IBaseService<WorkSessionModel, WorkSession, int>
    {
        Task<List<WorkSessionModel>> InsertList(List<WorkSessionModel> lstworkSessions);
        Task<List<WorkSession>> GetWorkSessions(DateTime date);
        Task<WorkSession> GetLastWorkSession(int logId);
        void DeleteSessions(List<WorkSession> sessions);
        Task<List<WorkSession>> WorkSessionsList(int[] timelogIds);
    }
}
