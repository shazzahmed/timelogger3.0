using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Data.IRepository
{
    public interface IInvitationRepository : IBaseRepository<Invitation, int>
    {
        Task<List<Invitation>> GetActiveProjects(string userId);
        Task<List<Invitation>> GetInvitationsList(string userId);
    }
}
