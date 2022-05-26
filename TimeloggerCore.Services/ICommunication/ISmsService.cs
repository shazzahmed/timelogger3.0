using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Services.ICommunication
{
    public interface ISmsService
    {
        Task<bool> SendSms();
    }
}
