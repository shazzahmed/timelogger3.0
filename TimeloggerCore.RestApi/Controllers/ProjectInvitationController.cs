using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Models;
using TimeloggerCore.Services.IService;

namespace TimeloggerCore.RestApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class ProjectInvitationController : BaseController
    {
        private readonly IProjectInvitationService _projectInvitationService;

        public ProjectInvitationController(
            IProjectInvitationService projectInvitationService
            )
        {
            _projectInvitationService = projectInvitationService;
        }
    }
}
