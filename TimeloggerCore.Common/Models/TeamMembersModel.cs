using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class TeamMembersModel
    {
        public ApplicationUserModel User { get; set; }
        public List<InvitationModel> TeamMembers { get; set; }
        public ProjectModel Project { get; set; }
    }
}
