using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Common.Models
{
    public class InvitationModel
    {
        public int ID { get; set; }
        public ProjectModel Project { get; set; }
        public bool ExistingUser { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public ApplicationUser Client { get; set; }
        public ApplicationUser User { get; set; }
        public string UserID { get; set; }
        public string ClientID { get; set; }
        public int ProjectID { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string EmailAddress { get; set; }
        public bool IsAccepted { get; set; }
        public bool CanEditTimeLog { get; set; }
        public bool CanAddManualTime { get; set; }
        public double RatePerHour { get; set; }
        public double MinimumHours { get; set; }
        public double MaximumHours { get; set; }
        public MemberStatus Status { get; set; }
        public string FullName { get; set; }
        public ProjectOwner ProjectOwner { get; set; }
        public int ProjectHours { get; set; }
    }

    public class ActiveProjectsViewModel : InvitationModel
    {
        public bool IsActive { get; set; }
    }

    public class InvitationRequestModel
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public bool IsAccepted { get; set; }
        public bool ExistingUser { get; set; }
        public InvitationType InvitationType { get; set; }
        public ApplicationUser InvitationSentFrom { get; set; }
        public ApplicationUser InvitationSentTo { get; set; }
        public string Role { get; set; }
    }
    public class InvitationsModel
    {
        public InvitationModel Invitation { get; set; }
        public List<InvitationModel> Invitations { get; set; }
        public List<ClientWorkerModel> ClientWorkers { get; set; }
    }
}
