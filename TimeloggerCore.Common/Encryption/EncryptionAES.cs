﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TimeloggerCore.Common.Options;

namespace TimeloggerCore.Common.Encryption
{
    public class EncryptionAES : IEncryptionService
    {
        private readonly SecurityOptions securityOptions;
        private static int EncryptionIterationSize;
        private static string EncryptionPassword;
        private static string EncryptionSaltKey;
        private static string EncryptionVIKey;

        public EncryptionAES(IOptionsSnapshot<SecurityOptions> securityOptions)
        {
            this.securityOptions = securityOptions.Value;
            EncryptionIterationSize = this.securityOptions.EncryptionIterationSize;
            EncryptionPassword = this.securityOptions.EncryptionPassword;
            EncryptionSaltKey = this.securityOptions.EncryptionSaltKey;
            EncryptionVIKey = this.securityOptions.EncryptionVIKey;
        }

        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var rfc2898DerivedBytes = new Rfc2898DeriveBytes(EncryptionPassword,
                                                Encoding.ASCII.GetBytes(EncryptionSaltKey), EncryptionIterationSize))
            {
                byte[] keyBytes = rfc2898DerivedBytes.GetBytes(256 / 8);
                using (var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros })
                {
                    var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(EncryptionVIKey));
                    byte[] cipherTextBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBytes = memoryStream.ToArray();
                            cryptoStream.Close();
                        }
                        memoryStream.Close();
                    }
                    return Convert.ToBase64String(cipherTextBytes);
                }
            }
        }
        public string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            using (var rfc2898DerivedBytes = new Rfc2898DeriveBytes(EncryptionPassword,
                                                Encoding.ASCII.GetBytes(EncryptionSaltKey), EncryptionIterationSize))
            {
                byte[] keyBytes = rfc2898DerivedBytes.GetBytes(256 / 8);
                using (var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None })
                {
                    var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(EncryptionVIKey));
                    using (var memoryStream = new MemoryStream(cipherTextBytes))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
                        }
                    }
                }
            }
        }
    }
}
