using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Utilities.Logging
{
    public interface ILogging
    {
         void LogError(string UserID, string ApplicationName, string Method, string ErrorMessage);
    }
}
