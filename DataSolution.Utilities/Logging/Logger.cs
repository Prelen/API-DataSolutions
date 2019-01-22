using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Interfaces.Utilities.Logging;
using log4net;

namespace DataSolution.Utilities.Logging
{
    public  class Logger : ILogging
    {
        private  readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public  void LogError(string UserID,string ApplicationName,string Method,string ErrorMessage)
        {

            var error = $"User ID: {UserID}/r/n Application Name: {ApplicationName}/r/n Method: {Method} /r/n Error Message: {ErrorMessage},";
            log.Error(error);


        }
    }
}
