using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using DataSolution.PDFService;
using System.Configuration;
using DataSolution.Utilities.Logging;
using System.ServiceModel;

namespace DataSolution.Services
{
    public class TransUnionPDFService
    {
        ReportRequestServiceClient client = new ReportRequestServiceClient();

        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();

        public async Task<bool> ConvertPDFAsync(string Username, string UserEmail, string TicketNo, string UserID)
        {
            bool result = false;
            Logger log = new Logger();
            try
            {
                /* Begin Proxy code*/
                //BasicHttpBinding binding = new BasicHttpBinding();
                //binding.Security.Mode = BasicHttpSecurityMode.Transport;
                //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
                //binding.UseDefaultWebProxy = false;
                //binding.ProxyAddress = new Uri("http://proxy.ntcweb.co.za:8080");
                //EndpointAddress endpoint = new EndpointAddress("https://www.tudirect.co.za/TUReportService/Services/ReportRequest/ReportRequestService.svc");
                //ReportRequestServiceClient soap = new ReportRequestServiceClient(binding, endpoint);
                //soap.ClientCredentials.UserName.UserName = "svcwssdotnet";
                //soap.ClientCredentials.UserName.Password = "svcW$$d0tn3t";
                //var response = await soap.RequestReportBytesAsync(subNo, securityCode, Username, UserEmail, TicketNo);
                /*End Proxy Code*/

                var response = await client.RequestReportBytesAsync(subNo, securityCode, "Prelen", "Prelen@gmail.com", TicketNo);
               
                response.IsSuccess = result;

                if (result)
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/PDF");
                    File.WriteAllBytes(path + @"/Report.pdf", response.PDFBytes);
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

        public async Task<string> GetReportLink(string UserID,string SubscriberNo,string SecurityCode,string TicketNo)
        {

            string link = string.Empty;
            var log = new Logger();
            try
            {
                /* Begin Proxy code*/
                //BasicHttpBinding binding = new BasicHttpBinding("ReportRequestService");
                //binding.Security.Mode = BasicHttpSecurityMode.Transport;
                //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
                //binding.UseDefaultWebProxy = false;
                //binding.ProxyAddress = new Uri("http://proxy.ntcweb.co.za:8080");
                //EndpointAddress endpoint = new EndpointAddress("https://www.tudirect.co.za/TUReportService/Services/ReportRequest/ReportRequestService.svc");
                //ReportRequestServiceClient soap = new ReportRequestServiceClient(binding, endpoint);
                //soap.ClientCredentials.UserName.UserName = "svcwssdotnet";
                //soap.ClientCredentials.UserName.Password = "svcW$$d0tn3t";
                //var response = await soap.RequestReportLinkAsync(SubscriberNo, SecurityCode, "Prelen", "Prelen@gmail.com", TicketNo);
                /*End Proxy Code*/

                var response = await client.RequestReportLinkAsync(SubscriberNo, SecurityCode,"Prelen","Prelen@gmail.com", TicketNo);
                if (response.IsSuccess)
                {
                    link = response.HttpLink;
                }
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services.PDFConverter", "GetReportLink", ex.Message);
            }

            return link;
        }
    }
}