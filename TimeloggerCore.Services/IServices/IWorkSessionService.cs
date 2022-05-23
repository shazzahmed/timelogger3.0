using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services.IService
{
    public interface IWorkSessionService : IBaseService<WorkSessionModel, WorkSession, int>
    {
        List<WorkSession> InsertList(List<WorkSession> lstworkSessions);
        List<WorkSession> GetWorkSessions(DateTime date);
        WorkSession GetLastWorkSession(int logId);
        void DeleteSessions(List<WorkSession> sessions);
        List<WorkSession> WorkSessionsList(int[] timelogIds);
    }
}
