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

namespace DataSolution.Service.Controllers
{
    public class TransunionAPIController : ApiController , ITransunionAPI
    {
        ConsumerSoapClient client = new ConsumerSoapClient();
        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();

       [AllowAnonymous]
       [HttpGet]
       [Route("api/TransunionAPI/BureauEnquiry37Async/{Request}/{UserID}")]
        public async Task<bool> BureauEnquiry37Async(TransunionRequest.BureauEnquiry37Request Request,string UserID)
        {

            bool result = false;
            Logger log = new Logger();
            try
            {
              

                Mapper.Initialize(
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
                var bureauEnquiry37 = Mapper.Map<BureauEnquiry37>(Request);
                bureauEnquiry37.SecurityCode = securityCode;
                bureauEnquiry37.SubscriberCode = subNo;

                var response = await client.ProcessRequestTrans37Async(bureauEnquiry37, Destination.Test);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;
                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController,BureauEnquiry37Async", response.ErrorCode + " " +  response.ErrorMessage);
                //Save the transaction

            }
            catch (Exception ex)
            {
                
                log.LogError(UserID, "DataSolutions.Services", "BureauEnquiry37Async", ex.Message);
            }
           

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/IndividualTraceSearchAsync/{Request}/UserID")]
        public async Task<bool> IndividualTraceSearchAsync(TransunionRequest.IndividualTraceSearchRequest Request, string UserID)
        {

            bool result = false;
            Logger log = new Logger();
            try
            {
                Mapper.Initialize(
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

                var individualTrace = Mapper.Map<IndividualTraceSearchInput>(Request);
                var response = await client.ProcessRequestIndividualTraceSearchInputAsync(individualTrace);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.IndividualTraceSearchAsync", response.ErrorCode + " " + response.ErrorMessage);


                //Save Transaction

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
        [Route("api/TransunionAPI/TraceOrder68Async/{Request}/UserID")]
        public async Task<bool> TraceOrder68Async(TransunionRequest.TraceOrder68Request Request ,string UserID)
        {
            bool result = false;
            Logger log = new Logger();

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
               

                Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<TraceOrder68Request, IndividualTraceProductOrder68>()
                        .ForMember(x => x.ConsumerNumber, y => y.MapFrom(z => z.ConsumerNo))
                        .ForMember(x => x.TelephoneCode1, y => y.MapFrom(z => z.TelCode1))
                        .ForMember(x => x.TelephoneCode2, y => y.MapFrom(z => z.TelCode2))
                        .ForMember(x => x.PostalCode, y => y.MapFrom(z => z.Code));
                    }
                    );

                var trace = Mapper.Map<IndividualTraceProductOrder68>(Request);
                trace.SecurityCode = securityCode;
                trace.SubscriberCode = subNo;
                trace.ModuleProducts = productCode;
                var response = await client.ProcessRequestIndividualTraceProductOrderTrans05Async(trace);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.TraceOrder68Async", response.ErrorCode + " " + response.ErrorMessage);
                //Save Transaction
                result = true;
            }
            catch (Exception ex)
            {
                
                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.TraceOrder68Async", ex.Message);
            }


            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessPayrollEmployerInformationAsync/{Request}/UserID")]
        public async Task<bool> ProcessPayrollEmployerInformationAsync(TransunionRequest.PayrollEmployerInformationRequest Request, string UserID)
        {

            bool result = false;
            Logger log = new Logger();
            try
            {

                Mapper.Initialize(
                    cfg =>
                    {

                        cfg.CreateMap<PayrollEmployerInformationRequest, EmployerEnquiry>();
                    }
                    );

                var employerEnquiry = Mapper.Map<EmployerEnquiry>(Request);
                employerEnquiry.SecurityCode = securityCode;
                employerEnquiry.SubscriberCode = subNo;
                var response = await client.ProcessPayrollEmployerInformationAsync(employerEnquiry);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if(!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayrollEmployerInformation", response.ErrorCode + " " + response.ErrorMessage);

                
                //Log Transaction
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayrollEmployerInformation", ex.Message);
            }
          

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessPayslipInformationAsync/{Request}/UserID")]
        public async Task<bool> ProcessPayslipInformationAsync(TransunionRequest.PayslipInformationRequest Request, string UserID)
        {
            bool result = false;
            Logger log = new Logger();

            try
            {
               
                Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<PayslipInformationRequest, PayslipEnquiry>()
                        .ForMember(x => x.IdentityNo, y => y.MapFrom(z => z.IdentityNumber))
                        .ForMember(x => x.Forenames, y => y.MapFrom(z => z.FirstName));
                        
                    }
                    );
                var payslipEnquiry = Mapper.Map<PayslipEnquiry>(Request);
                payslipEnquiry.SubscriberCode = subNo;
                payslipEnquiry.SecurityCode = securityCode;

                var response = await client.ProcessPayslipInformationAsync(payslipEnquiry);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayslipInformationAsync", response.ErrorCode + " " + response.ErrorMessage);

                //Log Transaction
            
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessPayslipInformationAsync", ex.Message);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans01Async/{Request}/UserID")]
        public async Task<bool> ProcessRequestTrans01Async(TransunionRequest.RequestTrans01 Request, string UserID)
        {
            bool result = false;
            Logger log = new Logger();
            try
            {

                Mapper.Initialize(
                    cg =>
                    {
                        cg.CreateMap<RequestTrans01, BureauEnquiry01>()
                        .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                        .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                        .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                        .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                    }
                    );

                var enquiry = Mapper.Map<BureauEnquiry01>(Request);
                MapperConfiguration cfg = new MapperConfiguration(new AutoMapper.Configuration.MapperConfigurationExpression());
                MapperConfigurationExpression expression = new MapperConfigurationExpression();
              

                var response = await client.ProcessRequestTrans01Async(enquiry);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans01Async", response.ErrorCode + " " + response.ErrorMessage);

                //Log transaction
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans01Async", ex.Message);
            }

            return result;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans04Async/{Request}/UserID")]
        public async Task<bool> ProcessRequestTrans04Async(TransunionRequest.RequestTrans01 Request, string UserID)
        {
            bool result = false;
            Logger log = new Logger();

           
            try
            {

                Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<RequestTrans01, BureauEnquiry04>()
                       .ForMember(x => x.ul_long_score, y => y.MapFrom(z => z.LongScore))
                       .ForMember(x => x.ul_medium_score, y => y.MapFrom(z => z.MediumScore))
                       .ForMember(x => x.ul_short_score, y => y.MapFrom(z => z.ShortScore))
                       .ForMember(x => x.ul_average_score, y => y.MapFrom(z => z.AverageScore));
                    }
                    );
                var enquiry = Mapper.Map<BureauEnquiry04>(Request);
                var response = await client.ProcessRequestTrans04Async(enquiry);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;
                if(!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans04Async", response.ErrorCode + " " + response.ErrorMessage);

                //Log transaction
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans04Async", ex.Message);
            }

          

            return result;


        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/TransunionAPI/ProcessRequestTrans07Async/{Request}/UserID")]
        public async Task<bool> ProcessRequestTrans07Async(TransunionRequest.RequestTrans07 Request, string UserID)
        {
            bool result = false;
            Logger log = new Logger();

            try
            {
                Mapper.Initialize(
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

                var enquiry07 = Mapper.Map<BureauEnquiry07>(Request);
                enquiry07.SecurityCode = securityCode;
                enquiry07.SubscriberCode = subNo;
                var response = await client.ProcessRequestTrans07Async(enquiry07);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if(!result)
                    log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans07Async", response.ErrorCode + " " + response.ErrorMessage);

                //log transaction
            }
            catch (Exception ex)
            {

                log.LogError(UserID, "DataSolutions.Services", "TransunionAPIController.ProcessRequestTrans07Async", ex.Message);
            }

            return result;

        }



    }
}
