using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataSolution.Utilities.Logging;
using DataSolution.Service.PDFService;
using System.Threading.Tasks;
using System.IO;
using DataSolution.Domain.Interfaces.Service;

namespace DataSolution.Service.Controllers
{
    public class PDFConverterController : ApiController, IPDFConverter
    {
        ReportRequestServiceClient client = new ReportRequestServiceClient();

        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();

      

        public async Task<bool> ConvertPDFAsync(string Username, string UserEmail, string TicketNo,string UserID)
        {
            bool result = false;
            Logger log = new Logger();
            try
            {
                var response = await client.RequestReportBytesAsync(subNo, securityCode, Username, UserEmail, TicketNo);
                response.IsSuccess = result;

                if (result)
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/PDF");
                    File.WriteAllBytes(path + @"/Report.pdf",response.PDFBytes);
                }
                else
                {
                    
                    log.LogError(UserID, "DataSolutions.Services.PDFConverter", "ConvertPDFAsync", "Unable to get the PDF bytes returned.");
                }
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services.PDFConverter", "ConvertPDFAsync", ex.Message);
            }
            return result;
        }
    }
}
