using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }
}
