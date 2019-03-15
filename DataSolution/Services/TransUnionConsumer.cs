using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
//using System.Web.Http;
using DataSolution.Utilities.Logging;
using DataSolution.Domain.Interfaces.Service;
using DataSolution.Domain.Model.Services;
using static DataSolution.Domain.Model.Services.TransunionRequest;
using AutoMapper;
using AutoMapper.Configuration;
using DataSolution.Domain.Model.Data;
using DataSolution.Data.DAL;
using DataSolution.Service.TransunionConsumer;
using System.Configuration;
using System.Net;
using System.ServiceModel;

namespace DataSolution.Services
{
    public class TransUnionConsumer
    {
        ConsumerSoapClient client = new ConsumerSoapClient();
        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();
        readonly string environment = ConfigurationManager.AppSettings["Environment"].ToString();
        Destination destination = new Destination();
        private Logger log;
        bool result;
        DateTime startDate;
        DateTime endDate;
        TransactionModel.TransactionData transData;


        public async Task<bool> GetConsumerProfile(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID)
        {

            startDate = DateTime.Now;

            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                        cfg =>
                        {
                            cfg.CreateMap<RequestTrans01, BureauEnquiry13>()
                           .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                           .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                           .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                           .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                        }
                        );

                var mapper = config.CreateMapper();

                var enquiry13 = mapper.Map<BureauEnquiry13>(Request);
                enquiry13.SecurityCode = securityCode;
                enquiry13.SubscriberCode = subNo;
                enquiry13.EnquirerContactName = "Prelen Nair";
                enquiry13.EnquirerContactPhoneNo = "083225398";
                var response = await client.ProcessRequestTrans13Async(enquiry13);
               
                result = response.ErrorCode == null ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Web", "TransUnionConsumer.GetConsumerProfile", response.ErrorCode + " " + response.ErrorMessage);

                //log transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Web", "TransUnionConsumer.GetConsumerProfile", ex.Message);
            }

            return result;
        }


        public async Task<bool> GetConsumerProfileWithAddress(TransunionRequest.BureauEnquiry37Request Request, string UserID, int ProductID)
        {

            startDate = DateTime.Now;

            log = new Logger();
            try
            {


                var config = new MapperConfiguration(
                       cfg =>
                       {
                           cfg.CreateMap<BureauEnquiry37Request, BureauEnquiry37>()
                           .ForMember(x => x.NoOfDependants, y => y.MapFrom(z => z.Dependents))
                           .ForMember(x => x.ProvinceCode, y => y.MapFrom(z => z.Province))
                           .ForMember(x => x.Address1Period, y => y.MapFrom(z => z.AddressPeriod))
                           .ForMember(x => x.BankAccountNumber, y => y.MapFrom(z => z.BankNo))
                           .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.Email));
                       }
                       );

                var mapper = config.CreateMapper();
                var bureauEnquiry37 = mapper.Map<BureauEnquiry37>(Request);
                bureauEnquiry37.SecurityCode = securityCode;
                bureauEnquiry37.SubscriberCode = subNo;
                bureauEnquiry37.EnquirerContactName = "Prelen Nair";
                bureauEnquiry37.EnquirerContactPhoneNo = "083225398";

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                /* Begin Proxy code*/
                //BasicHttpBinding binding = new BasicHttpBinding("ConsumerSoap");
                //binding.Security.Mode = BasicHttpSecurityMode.Transport;
                //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
                //binding.UseDefaultWebProxy = false;
                //binding.ProxyAddress = new Uri("http://proxy.ntcweb.co.za:8080");
                //EndpointAddress endpoint = new EndpointAddress("https://securetest.transunion.co.za/TUBureau118/Consumer.asmx");
                //ConsumerSoapClient soap = new ConsumerSoapClient(binding, endpoint);
                //soap.ClientCredentials.UserName.UserName = "svcwssdotnet";
                //soap.ClientCredentials.UserName.Password = "svcW$$d0tn3t";
                //var response = await client.ProcessRequestTrans37Async(bureauEnquiry37, destination);
                /*End Proxy Code*/


                var response = await client.ProcessRequestTrans37Async(bureauEnquiry37, destination);
                result = response.ErrorCode.Trim() == string.Empty ? true : false;

                endDate = DateTime.Now;
                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController,ProcessRequestTrans37Async", response.ErrorCode + " " + response.ErrorMessage);
                //Save the transaction
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);

            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans37Async", ex.Message);
            }


            return result;
        }


        public async Task<bool> PersonalTraceOrder(TransunionRequest.TraceOrder68Request Request, string UserID, int ProductID)
        {
            startDate = DateTime.Now;

            log = new Logger();

           
            if (Request.ProductCode != null)
            {
                ModuleProductCode code = new ModuleProductCode();
                int count = Request.ProductCode != null ? Request.ProductCode.Count : 0;
                ModuleProductCode[] productCode = new ModuleProductCode[count];

                int i = 0;
                foreach (string item in Request.ProductCode)
                {
                    code.Code = item;
                    productCode[i] = code;
                    code = new ModuleProductCode();
                }
            }
           
            try
            {

                var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<TraceOrder68Request, IndividualTraceProductOrder68>()
                         .ForMember(x => x.ConsumerNumber, y => y.MapFrom(z => z.ConsumerNo))
                         .ForMember(x => x.TelephoneCode1, y => y.MapFrom(z => z.TelCode1))
                         .ForMember(x => x.TelephoneCode2, y => y.MapFrom(z => z.TelCode2))
                         .ForMember(x => x.PostalCode, y => y.MapFrom(z => z.Code));
                     }
                     );

                var mapper = config.CreateMapper();

                var trace = mapper.Map<IndividualTraceProductOrder68>(Request);
                trace.SecurityCode = securityCode;
                trace.SubscriberCode = subNo;
                var products = new List<ModuleProductCode>();
                var module = new ModuleProductCode();
                module.Code = "6502";
                products.Add(module);
                module = new ModuleProductCode();
                module.Code = "6503";
                products.Add(module);
                module = new ModuleProductCode();
                module.Code = "6504";
                products.Add(module);
                module = new ModuleProductCode();
                module.Code = "6505";
                products.Add(module);
                trace.ModuleProducts = products.ToArray();

                /* Begin Proxy code*/
                //BasicHttpBinding binding = new BasicHttpBinding("ConsumerSoap");
                //binding.Security.Mode = BasicHttpSecurityMode.Transport;
                //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
                //binding.UseDefaultWebProxy = false;
                //binding.ProxyAddress = new Uri("http://proxy.ntcweb.co.za:8080");
                //EndpointAddress endpoint = new EndpointAddress("https://securetest.transunion.co.za/TUBureau118/Consumer.asmx");
                //ConsumerSoapClient soap = new ConsumerSoapClient(binding, endpoint);
                //soap.ClientCredentials.UserName.UserName = "svcwssdotnet";
                //soap.ClientCredentials.UserName.Password = "svcW$$d0tn3t";
                //var response = await soap.ProcessRequestIndividualTraceProductOrderTrans05Async(trace);
                /*End Proxy Code*/


                var response = await client.ProcessRequestIndividualTraceProductOrderTrans05Async(trace);
                result = response.ErrorCode == null ? true : false;

                if (result)
                {
                    string str = await new TransUnionPDFService().GetReportLink(UserID, subNo, securityCode, Request.TicketNo);
                    bool x = await new TransUnionPDFService().ConvertPDFAsync("Prelen", "Prelen@gmail.com", Request.TicketNo, UserID);
                }
                else
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.TraceOrder68Async", response.ErrorCode + " " + response.ErrorMessage);
                //Save Transaction

                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.TraceOrder68Async", ex.Message);
            }


            return result;

        }


        public async Task<bool> ProcessRequestTrans41Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                          cfg =>
                          {
                              cfg.CreateMap<RequestTrans01, BureauEnquiry41>()
                            .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                            .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                            .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                            .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                          }
                          );

                var mapper = config.CreateMapper();
                var enquiry41 = mapper.Map<BureauEnquiry41>(Request);
                enquiry41.SecurityCode = securityCode;
                enquiry41.SubscriberCode = subNo;
                enquiry41.EnquirerContactName = "Prelen Nair";
                enquiry41.EnquirerContactPhoneNo = "0832253698";
                
                destination = environment == "Test" ? Destination.Test : Destination.Live;

                /* Begin Proxy code*/
                //BasicHttpBinding binding = new BasicHttpBinding("ConsumerSoap");
                //binding.Security.Mode = BasicHttpSecurityMode.Transport;
                //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
                //binding.UseDefaultWebProxy = false;
                //binding.ProxyAddress = new Uri("http://proxy.ntcweb.co.za:8080");
                //EndpointAddress endpoint = new EndpointAddress("https://securetest.transunion.co.za/TUBureau118/Consumer.asmx");
                //ConsumerSoapClient soap = new ConsumerSoapClient(binding, endpoint);
                //soap.ClientCredentials.UserName.UserName = "svcwssdotnet";
                //soap.ClientCredentials.UserName.Password = "svcW$$d0tn3t";
                //var response = await soap.ProcessRequestTrans41Async(enquiry41, destination);
                /*End Proxy Code*/



                var response = await client.ProcessRequestTrans41Async(enquiry41, destination);

                result = response.ErrorCode == null ? true : false;

                if (result)
                {
                    string ticketno = "";
                    if (response.TicketSuccessConfirmationFR != null)
                    {
                        ticketno = response.TicketSuccessConfirmationFR.TicketNumber;
                        
                    }
                    if (response.TraceInformationTI != null)
                    {
                        ticketno = response.TraceInformationTI[0].OTicketNumber;
                    }
                    bool x = await new TransUnionPDFService().ConvertPDFAsync("Prelen", "Prelen@gmail.com", ticketno, UserID);
                    string str = await new TransUnionPDFService().GetReportLink(UserID, subNo, securityCode, ticketno);
                }
                else
                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans41Async", response.ErrorCode + " " + response.ErrorMessage);

                // Save Transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans41Async", ex.Message);
            }

            return result;
        }

        public async Task<bool> IndividualTraceSearchAsync(TransunionRequest.IndividualTraceSearchRequest Request, string UserID, int ProductID)
        {
            startDate = DateTime.Now;

            log = new Logger();
            try
            {


                var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<IndividualTraceSearchRequest, IndividualTraceSearchInput>()
                        .ForMember(x => x.TicketNumber, y => y.MapFrom(z => z.TicketNo))
                        .ForMember(x => x.ReportNumber, y => y.MapFrom(z => z.ReportNo))
                        .ForMember(x => x.UserEmail, y => y.MapFrom(z => z.Email))
                        .ForMember(x => x.ConsumerNumber, y => y.MapFrom(z => z.ConsumerNo))
                        .ForMember(x => x.TelephoneAreaCode, y => y.MapFrom(z => z.PhoneCode))
                        .ForMember(x => x.TelephoneNumber, y => y.MapFrom(z => z.TelNo))
                        .ForMember(x => x.CellNumber, y => y.MapFrom(z => z.CellNo))
                        .ForMember(x => x.AddressStreetNumber, y => y.MapFrom(z => z.StreetAddress))
                        .ForMember(x => x.AddressSuburb, y => y.MapFrom(z => z.Suburb))
                        .ForMember(x => x.AddressTown, y => y.MapFrom(z => z.Town))
                        .ForMember(x => x.AddressPostalCode, y => y.MapFrom(z => z.PostalCode))
                        .ForMember(x => x.IdentityNumber, y => y.MapFrom(z => z.IDNo))
                        .ForMember(x => x.DateOfBirth, y => y.MapFrom(z => z.DateOfBirth)); ;
                     }
                     );

                var mapper = config.CreateMapper();

                var individualTrace = mapper.Map<IndividualTraceSearchInput>(Request);
                individualTrace.SubscriberCode = subNo;
                individualTrace.SecurityCode = securityCode;
                individualTrace.SearchType = "01";
                individualTrace.SearchReason = "61";

                /* Begin Proxy code*/
                //BasicHttpBinding binding = new BasicHttpBinding("ConsumerSoap"); 
                //binding.Security.Mode = BasicHttpSecurityMode.Transport;
                //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
                //binding.UseDefaultWebProxy = false;
                //binding.ProxyAddress = new Uri("http://proxy.ntcweb.co.za:8080");
                //EndpointAddress endpoint = new EndpointAddress("https://securetest.transunion.co.za/TUBureau118/Consumer.asmx");
                //ConsumerSoapClient soap = new ConsumerSoapClient(binding, endpoint);
                //soap.ClientCredentials.UserName.UserName = "svcwssdotnet";
                //soap.ClientCredentials.UserName.Password = "svcW$$d0tn3t";
                //var response = await soap.ProcessRequestIndividualTraceSearchInputAsync(individualTrace);
                /*End Proxy Code*/

                  var response = await client.ProcessRequestIndividualTraceSearchInputAsync(individualTrace);


                result = response.ErrorCode == null ? true : false;

                if (result)
                {
                    string ticketno = string.Empty;
                    string consumerno = string.Empty;
                    if (response.TraceInformationTI != null)
                    {
                        ticketno  = response.TraceInformationTI[0].OTicketNumber;
                    }

                    if (response.TraceDetailsTD != null)
                    {
                        consumerno = response.TraceDetailsTD[0].OConsumerNumber;
                    }

                    TransunionRequest.TraceOrder68Request trace68 = new TraceOrder68Request
                    {
                        TicketNo = ticketno,
                        ConsumerNo = consumerno,
                        IDNo1 = individualTrace.IdentityNumber,
                        AddressLine1 = individualTrace.AddressLine1,
                        AddressLine2 = individualTrace.AddressLine2,
                        Suburb = individualTrace.AddressSuburb,
                        CellNo = Request.CellNo
                    };

                    var products = await PersonalTraceOrder(trace68, UserID, ProductID);
                }
                else
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.IndividualTraceSearchAsync", response.ErrorCode + " " + response.ErrorMessage);


                //Save Transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);

                result = true;
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController,IndividualTraceSearchAsync", ex.Message);
            }

            return result;

        }


        private bool SaveTransaction(TransactionModel.TransactionData Transaction)
        {

            return new TransactionData().InsertTransaction(Transaction, Transaction.UserID.ToString());
        }
    }
}