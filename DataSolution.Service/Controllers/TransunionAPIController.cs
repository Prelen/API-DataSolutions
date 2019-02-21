using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataSolution.Service.TransunionConsumer;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.Web.Services.Protocols;
using DataSolution.Utilities.Logging;
using DataSolution.Domain.Interfaces.Service;
using DataSolution.Domain.Model.Services;
using static DataSolution.Domain.Model.Services.TransunionRequest;
using AutoMapper;
using AutoMapper.Configuration;
using DataSolution.Domain.Model.Data;
using DataSolution.Data.DAL;

namespace DataSolution.Service.Controllers
{
    public class TransunionAPIController : ApiController , ITransunionAPI
    {
        ConsumerSoapClient client = new ConsumerSoapClient();
        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();
        readonly string environment = ConfigurationManager.AppSettings["Environment"].ToString();
        Destination destination = new Destination();
        private Logger log;
        bool result;
        TransactionModel.TransactionData transData;
        DateTime startDate;
        DateTime endDate;

       [AllowAnonymous]
       [HttpGet]
       [Route("api/TransunionAPI/ProcessRequestTrans37Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans37Async(TransunionRequest.BureauEnquiry37Request Request,string UserID,int ProductID)
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

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans37Async(bureauEnquiry37, destination);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                endDate = DateTime.Now;
                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController,ProcessRequestTrans37Async", response.ErrorCode + " " +  response.ErrorMessage);
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

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/IndividualTraceSearchAsync/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> IndividualTraceSearchAsync(TransunionRequest.IndividualTraceSearchRequest Request, string UserID,int ProductID)
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
                        .ForMember(x => x.AddressPostalCode, y => y.MapFrom(z => z.PostalCode));
                     }
                     );

                var mapper = config.CreateMapper();

                var individualTrace = mapper.Map<IndividualTraceSearchInput>(Request);
                var response = await client.ProcessRequestIndividualTraceSearchInputAsync(individualTrace);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
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


        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/TraceOrder68Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> TraceOrder68Async(TransunionRequest.TraceOrder68Request Request ,string UserID,int ProductID)
        {
            startDate = DateTime.Now;

             log = new Logger();
            
            ModuleProductCode code = new ModuleProductCode();
            int count = Request.ProductCode.Count;
            ModuleProductCode[] productCode = new ModuleProductCode[count];
            
            int i = 0;
            foreach (string  item in Request.ProductCode)
            {
                code.Code = item;
                productCode[i] = code;
                code = new ModuleProductCode();
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
                trace.ModuleProducts = productCode;
                var response = await client.ProcessRequestIndividualTraceProductOrderTrans05Async(trace);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
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

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessPayrollEmployerInformationAsync/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessPayrollEmployerInformationAsync(TransunionRequest.PayrollEmployerInformationRequest Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
          
             log = new Logger();
            try
            {

               

                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<PayrollEmployerInformationRequest, EmployerEnquiry>();
                    }
                    );

                var mapper = config.CreateMapper();

                var employerEnquiry = mapper.Map<EmployerEnquiry>(Request);
                employerEnquiry.SecurityCode = securityCode;
                employerEnquiry.SubscriberCode = subNo;
                var response = await client.ProcessPayrollEmployerInformationAsync(employerEnquiry);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if(!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayrollEmployerInformation", response.ErrorCode + " " + response.ErrorMessage);


                //Log Transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayrollEmployerInformation", ex.Message);
            }
          

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessPayslipInformationAsync/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessPayslipInformationAsync(TransunionRequest.PayslipInformationRequest Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
             log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                   cfg =>
                   {
                       cfg.CreateMap<PayslipInformationRequest, PayslipEnquiry>()
                       .ForMember(x => x.IdentityNo, y => y.MapFrom(z => z.IdentityNumber))
                       .ForMember(x => x.Forenames, y => y.MapFrom(z => z.FirstName));
                   }
                   );

                var mapper = config.CreateMapper();

                var payslipEnquiry = mapper.Map<PayslipEnquiry>(Request);
                payslipEnquiry.SubscriberCode = subNo;
                payslipEnquiry.SecurityCode = securityCode;

                var response = await client.ProcessPayslipInformationAsync(payslipEnquiry);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayslipInformationAsync", response.ErrorCode + " " + response.ErrorMessage);

                //Log Transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayslipInformationAsync", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans01Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans01Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
             log = new Logger();
            try
            {


                var config = new MapperConfiguration(
                           cg =>
                           {
                               cg.CreateMap<RequestTrans01, BureauEnquiry01>()
                                .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                                .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                                .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                                .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                           }
                           );

                var mapper = config.CreateMapper();

                var enquiry = mapper.Map<BureauEnquiry01>(Request);
                MapperConfiguration cfg = new MapperConfiguration(new AutoMapper.Configuration.MapperConfigurationExpression());
                MapperConfigurationExpression expression = new MapperConfigurationExpression();
              

                var response = await client.ProcessRequestTrans01Async(enquiry);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans01Async", response.ErrorCode + " " + response.ErrorMessage);

                //Log transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans01Async", ex.Message);
            }

            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans04Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans04Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
          
             log = new Logger();

            try
            {


                var config = new MapperConfiguration(
                           cg =>
                           {
                               cg.CreateMap<RequestTrans01, BureauEnquiry04>()
                                .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                                .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                                .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                                .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                           }
                           );

                var mapper = config.CreateMapper();
                var enquiry = mapper.Map<BureauEnquiry04>(Request);
                enquiry.SecurityCode = securityCode;
                enquiry.SubscriberCode = subNo;
                var response = await client.ProcessRequestTrans04Async(enquiry);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;
                if(!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans04Async", response.ErrorCode + " " + response.ErrorMessage);

                //Log transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans04Async", ex.Message);
            }

          

            return result;


        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans07Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans07Async(TransunionRequest.BureauEnquiry37Request Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                          cfg =>
                          {
                              cfg.CreateMap<BureauEnquiry37Request, BureauEnquiry07>()
                            .ForMember(x => x.NoOfDependants, y => y.MapFrom(z => z.Dependents))
                            .ForMember(x => x.ProvinceCode, y => y.MapFrom(z => z.Province))
                            .ForMember(x => x.Address1Period, y => y.MapFrom(z => z.AddressPeriod))
                            .ForMember(x => x.BankAccountNumber, y => y.MapFrom(z => z.BankNo))
                            .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.Email));
                          }
                          );

                var mapper = config.CreateMapper();

                var enquiry07 = mapper.Map<BureauEnquiry07>(Request);
                enquiry07.SecurityCode = securityCode;
                enquiry07.SubscriberCode = subNo;
                var response = await client.ProcessRequestTrans07Async(enquiry07);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if(!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans07Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans07Async", ex.Message);
            }

            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans12Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans12Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {

            startDate = DateTime.Now;
          
            log = new Logger();

            try
            {


                var config = new MapperConfiguration(
                         cfg =>
                         {
                             cfg.CreateMap<BureauEnquiry37Request, BureauEnquiry12>()
                             .ForMember(x => x.NoOfDependants, y => y.MapFrom(z => z.Dependents))
                             .ForMember(x => x.ProvinceCode, y => y.MapFrom(z => z.Province))
                             .ForMember(x => x.Address1Period, y => y.MapFrom(z => z.AddressPeriod))
                             .ForMember(x => x.BankAccountNumber, y => y.MapFrom(z => z.BankNo))
                             .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.Email));
                         }
                         );

                var mapper = config.CreateMapper();

                var enquiry12 = mapper.Map<BureauEnquiry12>(Request);
                enquiry12.SecurityCode = securityCode;
                enquiry12.SubscriberCode = subNo;
                var response = await client.ProcessRequestTrans12Async(enquiry12);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans12Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans12Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans13Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans13Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
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
                var response = await client.ProcessRequestTrans13Async(enquiry13);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans13Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans13Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans17Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans17Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {

            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                       cfg =>
                       {
                           cfg.CreateMap<RequestTrans01, BureauEnquiry17>()
                       .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                       .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                       .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                       .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                       }
                       );

                var mapper = config.CreateMapper();

                var enquiry17 = mapper.Map<BureauEnquiry17>(Request);
                enquiry17.SecurityCode = securityCode;
                enquiry17.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans17Async(enquiry17, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans17Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans17Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans18Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans18Async(TransunionRequest.RequestTrans18 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;

            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                           cfg =>
                           {
                               cfg.CreateMap<RequestTrans18, BureauEnquiry18>()
                               .ForMember(x => x.Uif, y => y.MapFrom(z => z.UIF))
                                .ForMember(x => x.Schoolfees, y => y.MapFrom(z => z.SchoolFees));
                           }
                           );

                var mapper = config.CreateMapper();
                var enquiry18 = mapper.Map<BureauEnquiry18>(Request);
                enquiry18.SecurityCode = securityCode;
                enquiry18.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans18Async(enquiry18, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans18Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans18Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans22Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans22Async(TransunionRequest.RequestTrans22 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();
            try
            {

                var config = new MapperConfiguration(
                           cfg =>
                           {
                               cfg.CreateMap<RequestTrans22, BureauEnquiry22>();
                           }
                           );

                var mapper = config.CreateMapper();

                var enquiry22 = mapper.Map<BureauEnquiry22>(Request);
                enquiry22.SecurityCode = securityCode;
                enquiry22.SubscriberCode = subNo;

                var response = await client.ProcessRequestTrans22Async(enquiry22);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans22Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans22Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans23Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans23Async(TransunionRequest.RequestTrans23 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                          cfg =>
                          {
                              cfg.CreateMap<RequestTrans23, BureauEnquiry23>()
                              .ForMember(x => x.SAIDNumber, y => y.MapFrom(z => z.IDNumber));
                          }
                          );

                var mapper = config.CreateMapper();

                var enquiry23 = mapper.Map<BureauEnquiry23>(Request);
                enquiry23.SecurityCode = securityCode;
                enquiry23.SubscriberCode = subNo;

                var response = await client.ProcessRequestTrans23Async(enquiry23);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans23Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans23Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans26Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans26Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {
          
                var config = new MapperConfiguration(
                          cfg =>
                          {
                              cfg.CreateMap<RequestTrans01, BureauEnquiry26>();
                          }
                          );

                var mapper = config.CreateMapper();

                var enquiry26 = mapper.Map<BureauEnquiry26>(Request);
                enquiry26.SecurityCode = securityCode;
                enquiry26.SubscriberCode = subNo;

               
                var response = await client.ProcessRequestTrans26Async(enquiry26);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans26Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans26Async", ex.Message);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans31Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans31Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                  cfg =>
                  {
                      cfg.CreateMap<RequestTrans01, BureauEnquiry31>();
                  }
                  );

                var mapper = config.CreateMapper();

                var enquiry31 = mapper.Map<BureauEnquiry31>(Request);
                enquiry31.SecurityCode = securityCode;
                enquiry31.SubscriberCode = subNo;


                var response = await client.ProcessRequestTrans31Async(enquiry31);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans31Async", response.ErrorCode + " " + response.ErrorMessage);

                //Log transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans31Async", ex.Message);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans38Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans38Async(TransunionRequest.BureauEnquiry37Request Request, string UserID,int ProductID)
        {

            startDate = DateTime.Now;

            log = new Logger();
            try
            {
                var config = new MapperConfiguration(
                  cfg =>
                  {
                      cfg.CreateMap<BureauEnquiry37Request, BureauEnquiry38>()
                       .ForMember(x => x.NoOfDependants, y => y.MapFrom(z => z.Dependents))
                       .ForMember(x => x.ProvinceCode, y => y.MapFrom(z => z.Province))
                       .ForMember(x => x.Address1Period, y => y.MapFrom(z => z.AddressPeriod))
                       .ForMember(x => x.BankAccountNumber, y => y.MapFrom(z => z.BankNo))
                       .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.Email));
                  }
                  );

                var mapper = config.CreateMapper();
                var bureauEnquiry38 = mapper.Map<BureauEnquiry38>(Request);
                bureauEnquiry38.SecurityCode = securityCode;
                bureauEnquiry38.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans38Async(bureauEnquiry38, destination);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;
                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController,ProcessRequestTrans38Async", response.ErrorCode + " " + response.ErrorMessage);
                //Save the transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans38Async", ex.Message);
            }


            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans41Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans41Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
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

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans41Async(enquiry41, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
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


        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans42Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans42Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {


                var config = new MapperConfiguration(
                      cfg =>
                      {
                          cfg.CreateMap<RequestTrans01, BureauEnquiry42>();
                      }
                      );

                var mapper = config.CreateMapper();

                var enquiry42 = mapper.Map<BureauEnquiry42>(Request);
                enquiry42.SecurityCode = securityCode;
                enquiry42.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans42Async(enquiry42, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans42Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans42Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans43Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans43Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<RequestTrans01, BureauEnquiry43>();
                    }
                    );

                var mapper = config.CreateMapper();

                var enquiry43 = mapper.Map<BureauEnquiry43>(Request);
                enquiry43.SecurityCode = securityCode;
                enquiry43.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans43Async(enquiry43, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans43Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans43Async", ex.Message);
            }

            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans47Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans47Async(TransunionRequest.BureauEnquiry37Request Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();
            try
            {

                var config = new MapperConfiguration(
                  cfg =>
                  {
                      cfg.CreateMap<BureauEnquiry37Request, BureauEnquiry47>()
                       .ForMember(x => x.NoOfDependants, y => y.MapFrom(z => z.Dependents))
                       .ForMember(x => x.ProvinceCode, y => y.MapFrom(z => z.Province))
                       .ForMember(x => x.Address1Period, y => y.MapFrom(z => z.AddressPeriod))
                       .ForMember(x => x.BankAccountNumber, y => y.MapFrom(z => z.BankNo))
                       .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.Email));
                  }
                  );

                var mapper = config.CreateMapper();
                var bureauEnquiry47 = mapper.Map<BureauEnquiry47>(Request);
                bureauEnquiry47.SecurityCode = securityCode;
                bureauEnquiry47.SubscriberCode = subNo;

             
                var response = await client.ProcessRequestTrans47Async(bureauEnquiry47);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;
                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController,ProcessRequestTrans47Async", response.ErrorCode + " " + response.ErrorMessage);
                //Save the transaction
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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans47Async", ex.Message);
            }


            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans91Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans91Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {
                var config = new MapperConfiguration(
                  cfg =>
                  {
                      cfg.CreateMap<RequestTrans01, BureauEnquiry91>();
                  }
                  );

                var mapper = config.CreateMapper();

                var enquiry91 = mapper.Map<BureauEnquiry91>(Request);
                enquiry91.SecurityCode = securityCode;
                enquiry91.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans91Async(enquiry91, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans91Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans91Async", ex.Message);
            }

            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans92Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTrans92Async(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<RequestTrans01, BureauEnquiry92>();
                 }
                 );

                var mapper = config.CreateMapper();

                var enquiry92 = mapper.Map<BureauEnquiry92>(Request);
                enquiry92.SecurityCode = securityCode;
                enquiry92.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTrans92Async(enquiry92, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans92Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans92Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTransC20Async/{IDNumber}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTransC20Async(string IDNumber, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();
            try
            {
                BureauEnquiryC20 bureauEnquiryC20 = new BureauEnquiryC20
                {
                    IdentityNumber = IDNumber,
                    SecurityCode = securityCode,
                    SubscriberCode = subNo
                };


                var response = await client.ProcessRequestTransC20Async(bureauEnquiryC20);


                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans92Async", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC20Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTransC29Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTransC29Async(TransunionRequest.RequestC29 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RequestC29, BureauEnquiryC29>()
                       .ForMember(x => x.TelNumber, y => y.MapFrom(z => z.TelephoneNumber))
                       .ForMember(x => x.TelType, y => y.MapFrom(z => z.TelephoneType));
                }
                );

                var mapper = config.CreateMapper();

                var bureauC29 = mapper.Map<BureauEnquiryC29>(Request);
                bureauC29.SecurityCode = securityCode;
                bureauC29.SubscriberCode = subNo;

                var response = await client.ProcessRequestTransC29Async(bureauC29);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans92Async", response.ErrorCode + " " + response.ErrorMessage);


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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC29Async", ex.Message);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTransC3Async/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTransC3Async(string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {
                BureauEnquiryC3 bureauEnquiryC3 = new BureauEnquiryC3
                {
                    SecurityCode = securityCode,
                    SubscriberCode = subNo
                };

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTransC3Async(bureauEnquiryC3, destination);

               
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC3Async", response.ErrorCode + " " + response.ErrorMessage);


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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC3Async", ex.Message);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTransC30Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTransC30Async(TransunionRequest.RequestTransC30 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                      cfg =>
                      {
                          cfg.CreateMap<RequestTransC30, BureauEnquiryC30>();
                      }
                      );

                var mapper = config.CreateMapper();

                var bureauC30 = mapper.Map<BureauEnquiryC30>(Request);
                bureauC30.SecurityCode = securityCode;
                bureauC30.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTransC30Async(bureauC30, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;


                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC30Async", response.ErrorCode + " " + response.ErrorMessage);


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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC30Async", ex.Message);
            }

            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTransC4Async/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTransC4Async(TransunionRequest.RequestTransC4 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {
                Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<RequestTransC30, RequestTransC4>();
                    }
                    );

                var bureauC4 = Mapper.Map<BureauEnquiryC4>(Request);
                bureauC4.SecurityCode = securityCode;
                bureauC4.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessRequestTransC4Async(bureauC4, destination);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;


                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC4Async", response.ErrorCode + " " + response.ErrorMessage);


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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC4Async", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTransC6Async/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestTransC6Async(string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {
                BureauEnquiryC6 bureauEnquiryC3 = new BureauEnquiryC6
                {
                    SecurityCode = securityCode,
                    SubscriberCode = subNo
                };

              
                var response = await client.ProcessRequestTransC6Async(bureauEnquiryC3);


                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC6Async", response.ErrorCode + " " + response.ErrorMessage);


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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTransC6Async", ex.Message);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestStrikeDate/{Request}/{UserID}/{ProductID}")]
        public async Task<bool> ProcessRequestStrikeDate(TransunionRequest.RequestTrans01 Request, string UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();

            try
            {

                var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<RequestTrans01, StrikeDateC16>();
                     }
                     );

                var mapper = config.CreateMapper();
                var strike = mapper.Map<StrikeDateC16>(Request);
                strike.SecurityCode = securityCode;
                strike.SubscriberCode = subNo;

                destination = environment == "Test" ? Destination.Test : Destination.Live;

                var response = await client.ProcessStrikeDateAsync(strike);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestStrikeDate", response.ErrorCode + " " + response.ErrorMessage);

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

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestStrikeDate", ex.Message);
            }

            return result;
        }

        private bool SaveTransaction(TransactionModel.TransactionData Transaction)
        {

            return new TransactionData().InsertTransaction(Transaction, Transaction.UserID.ToString());
        }
    }
}
