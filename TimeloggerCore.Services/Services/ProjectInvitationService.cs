using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services
{
    public class ProjectInvitationService : BaseService<ProjectsInvitationModel, ProjectsInvitation, int>, IProjectInvitationService
    {
        private readonly IProjectInvitationRepository _projectInvitationRepository;

        public ProjectInvitationService(IMapper mapper, IProjectInvitationRepository projectInvitationRepository, IUnitOfWork unitOfWork) : base(mapper, projectInvitationRepository, unitOfWork)
        {
            _projectInvitationRepository = projectInvitationRepository;
        }
    }
}
