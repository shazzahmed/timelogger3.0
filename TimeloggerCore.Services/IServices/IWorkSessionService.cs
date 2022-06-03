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
        Task<BaseModel> InsertList(List<WorkSessionModel> lstworkSessions);
        Task<BaseModel> GetWorkSessions(DateTime date);
        Task<BaseModel> GetLastWorkSession(int logId);
        void DeleteSessions(List<WorkSession> sessions);
        Task<BaseModel> WorkSessionsList(int[] timelogIds);
    }
}
