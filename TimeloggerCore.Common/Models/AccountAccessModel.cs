﻿using System.Collections.Generic;

namespace TimeloggerCore.Common.Models
{
    public class AccountAccessModel
	{
		public static AccessTokenModel TokenInfo { get; set; }
		public static ApplicationUserModel UserInfo { get; set; }
		public static LoginViewModel LoginInfo { get; set; }

		public static List<RoleModel> Roles { get; set; }
		public static List<TimeZoneModel> TimeZones { get; set; }
		public static List<ApplicationUserModel> Freelancers { get; set; }
		public static List<ApplicationUserModel> Clients { get; set; }
		public static List<ExternalLoginViewModel> ExternalLoginUrls { get; set; }
	}
}
