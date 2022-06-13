namespace TimeloggerCore.Common.Helpers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    public static class NetworkHelper
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string GetRemoteUserIp(string forwardedForIPs)
        {
            string userIp = string.Empty;

            if (forwardedForIPs != null)
            {
                userIp = forwardedForIPs.Split(":", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }

            return userIp;
        }
    }
}
