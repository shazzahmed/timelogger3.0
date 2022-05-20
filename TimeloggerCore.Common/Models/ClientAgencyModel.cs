using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class ClientAgencyModel
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        [ForeignKey("Agency")]
        public string AgencyId { get; set; }
        public bool IsAgencyAccepted { get; set; }
        virtual public ApplicationUser Client { get; set; }
        public ApplicationUser Agency { get; set; }
    }
    public class ClientAgenciesModel
    {
        public List<ClientAgencyModel> ClientAgencies { get; set; }
        public List<InvitationRequestModel> InvitationRequest { get; set; }
        public ApplicationUser CurrentUser { get; set; }
    }
    public class AgencyAlreadyExitModel
    {
        public string UserId { get; set; }
        public string AgencyEmail { get; set; }
    }
    public class ClientAgencyResponse
    {
        public string Message { get; set; }
        public System.Net.HttpStatusCode Response { get; set; }
    }
}
