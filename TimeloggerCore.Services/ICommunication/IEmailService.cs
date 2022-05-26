﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.ICommunication
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string subject, string content, string toEmail, string fromEmail = null, string fromName = null, string attachment = null);
    }
}
