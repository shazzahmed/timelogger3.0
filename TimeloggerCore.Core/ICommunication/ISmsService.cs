﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Core.ICommunication
{
    public interface ISmsService
    {
        Task<bool> SendSms();
    }
}
