using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Utilities
{
    public interface IEncryption
    {
        string Encrypt(string PlainText);
        string Decrypt(string EncryptedText);
    }
}
