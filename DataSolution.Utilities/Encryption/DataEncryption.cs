using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using DataSolution.Domain.Interfaces.Utilities;

namespace DataSolution.Utilities.Encryption
{
    
    public class DataEncryption : IEncryption
    {
        private const string key = "=!@4Fguip:}109NmkDW;>%}zxB>?4Rtd";
        private  byte[] iv = { 66, 25, 118, 201, 36, 75, 99, 120, 57, 9, 229, 186, 37, 74, 69, 152 };

        public string Encrypt(string PlainText)
        {
            var rj = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC


            };

            var bytesKey = Encoding.ASCII.GetBytes(key);
            var encryptor = rj.CreateEncryptor(bytesKey, iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            var toEncrypt = Encoding.ASCII.GetBytes(PlainText);
            cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
            cryptoStream.FlushFinalBlock();
            var encrypted = ms.ToArray();
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt (string EncryptedText)
        {
            var rj = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,

            };

            var bytesKey = Encoding.ASCII.GetBytes(key);
            var decryptor = rj.CreateDecryptor(bytesKey, iv);
            var toDecrypt = Convert.FromBase64String(EncryptedText);
            var len = new byte[toDecrypt.Length];
            MemoryStream ms = new MemoryStream(toDecrypt);
            CryptoStream cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            cryptoStream.Read(len, 0, len.Length);
            string text = Encoding.ASCII.GetString(len);


            return text.Replace("\0", ""); 
        }

    }
}