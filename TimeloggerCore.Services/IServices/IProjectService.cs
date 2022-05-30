﻿using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Services.IService
{
    public interface IProjectService : IBaseService<ProjectModel, Project, int>
    {
        Task<List<Project>> FreelancerProjects(string userId);
        Task<List<Project>> ProjectsWithCompanies();
        Task<List<Project>> AllProjects(string userRole);
        Task<Project> GetUserProjects(string Id);
        Task<List<Project>> GetAgencyProjecList(string userId);
        Task<List<Project>> GetUserProjecList(string userId);
    }
}
