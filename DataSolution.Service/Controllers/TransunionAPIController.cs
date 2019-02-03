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
                    BureauEnquiry37 request = new BureauEnquiry37
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    EnquirerContactName = Request.Enquirer,
                    EnquirerContactPhoneNo = Request.EnquirerPhoneNo,
                    EnquiryType = Request.EnquiryType,
                    EnquiryAmount = Request.EnquiryAmount,
                    Surname = Request.Surname,
                    Forename1 = Request.Forename1,
                    Forename2 = Request.Forename2,
                    MaidenName = Request.MaidenName,
                    BirthDate = Request.DateOfBirth,
                    IdentityNo1 = Request.IdentityNo1,
                    IdentityNo2 = Request.IdentityNo2,
                    Sex = Request.Sex,
                    Title = Request.Title,
                    MaritalStatus = Request.MaritalStatus,
                    NoOfDependants = Request.Dependents,
                    AddressLine1 = Request.AddressLine1,
                    AddressLine2 = Request.AddressLine2,
                    Suburb = Request.Suburb,
                    City = Request.City,
                    PostalCode = Request.HomePostalCode,
                    ProvinceCode = Request.Province,
                    Address1Period = Request.AddressPeriod,
                    OwnerTenant = Request.OwnerTenant,
                    HomeTelCode = Request.HomeTelCode,
                    HomeTelNo = Request.HomeTelNo,
                    WorkTelCode = Request.WorkTelCode,
                    WorkTelNo = Request.WorkTelNo,
                    SpouseForename1 = Request.SpouseForname1,
                    SpouseForename2 = Request.SpouseForname2,
                    Address2Line1 = Request.PostalAddress1,
                    Address2Line2 = Request.PostalAddress2,
                    Address2Suburb = Request.PostalSuburb,
                    Address2ProvinceCode = Request.PostaProvinceCode,
                    Occupation = Request.Occupation,
                    Employer = Request.Employer,
                    EmploymentPeriod = Request.EmploymentPeriod,
                    Salary = Request.Salary,
                    BankName = Request.BankName,
                    BankBranch = Request.BankBranch,
                    BankBranchCode = Request.BranchCode,
                    BankAccountNumber = Request.BankNo,
                    OperatorIdentity = Request.OperatorIdentity,
                    CellNo = Request.CellNo,
                    EmailAddress = Request.Email,
                    InsuranceSubNo = Request.InsuranceSubNo
                };


                var response = await client.ProcessRequestTrans37Async(request, Destination.Test);
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
                IndividualTraceSearchInput individualTrace = new IndividualTraceSearchInput
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    TicketNumber = Request.TicketNo,
                    ReportNumber = Request.ReportNo,
                    SearchIndicator = Request.SearchIndicator,
                    SearchType = Request.SearchType,
                    SearchReason = Request.SearchReason,
                    UserID = Request.SearchUserID,
                    UserBranch = Request.UserBranch,
                    UserName = Request.Username,
                    UserSurname = Request.UserSurname,
                    UserPhone = Request.UserPhone,
                    UserEmail = Request.Email,
                    ConsumerNumber = Request.ConsumerNo,
                    IdentityNumber = Request.IDNo,
                    Forename = Request.Forename,
                    Surname = Request.Surname,
                    TelephoneAreaCode = Request.PhoneCode,
                    TelephoneNumber = Request.TelNo,
                    CellNumber = Request.CellNo,
                    DateOfBirth = Request.DateOfBirth,
                    FromAge = Request.FromAge,
                    ToAge = Request.ToAge,
                    Gender = Request.Gender,
                    EmailAddress = Request.EmailAddress,
                    Employer = Request.Employer,
                    AddressStreetNumber = Request.StreetAddress,
                    AddressLine1 = Request.AddressLine1,
                    AddressLine2 = Request.AddressLine2,
                    AddressSuburb = Request.Suburb,
                    AddressTown = Request.Town,
                    AddressPostalCode = Request.PostalCode,
                    Counter = Request.Counter,
                    MoreCount = Request.MoreCount,
                    RepeatDetail = Request.RepeatDetail,
                    
                };

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
                IndividualTraceProductOrder68 trace = new IndividualTraceProductOrder68
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    TicketNo = Request.TicketNo,
                    ReportNo = Request.ReportNo,
                    ConsumerNumber = Request.ConsumerNo,
                    ModuleProducts = productCode,
                    IDNo1 = Request.IDNo1,
                    IDNo2 = Request.IDNo2,
                    TelephoneCode1 = Request.TelCode1,
                    TelephoneNumber1 = Request.TelNumber1,
                    TelephoneCode2 = Request.TelCode2,
                    TelephoneNumber2 = Request.TelNumber2,
                    CellNo = Request.CellNo,
                    AddressLine1 = Request.AddressLine1,
                    AddressLine2 = Request.AddressLine2,
                    Suburb = Request.Suburb,
                    PostalCode = Request.Code,
                    Province = Request.Province
                };

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
        public async Task<bool> ProcessPayrollEmployerInformationAsync(TransunionRequest.ProcessPayrollEmployerInformationRequest Request, string UserID)
        {

            bool result = false;
            Logger log = new Logger();
            try
            {
                EmployerEnquiry employerEnquiry = new EmployerEnquiry
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    BatchNumber = Request.BatchNumber,
                    BranchNumber = Request.BranchNumber,
                    ClientReference = Request.ClientReference,
                    ClientRequestID = Request.ClientReferenceID,
                    CompanyName = Request.CompanyName,
                    EmployerID = Request.EmployerID,
                    IsLiteVersion = Request.IsLiteVersion,
                    PageIndex = Request.PageIndex,
                    PayslipCategory = Request.PayslipCategory,
                    ProductCode = Request.ProductCode
                };

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
        public async Task<bool> ProcessPayslipInformationAsync(TransunionRequest.ProcessPayslipInformationRequest Request, string UserID)
        {
            bool result = false;
            Logger log = new Logger();

            try
            {
                PayslipEnquiry payslipEnquiry = new PayslipEnquiry
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    ContactName = Request.ContactName,
                    ContactNumber = Request.ContactNumber,
                    IdentityNo = Request.IdentityNumber,
                    Surname = Request.Surname,
                    Forenames = Request.FirstName,
                    AddressLine1 = Request.AddressLine1,
                    AddressLine2 = Request.AddressLine2,
                    AddressLine3 = Request.AddressLine3,
                    AddressLine4 = Request.AddressLine4,
                    PostalCode = Request.PostalCode,
                    EmployerDetailsID = Request.EmployerDetailsID,
                    CompanyName = Request.CompanyName,
                    CountryOfOrigin = Request.CountryOfOrigin,
                    TransactionItemID = Request.TransactionItemID,
                    PayslipCategory = Request.PayslipCategory,
                    IsLiteVersion = Request.IsLiteVersion
                  

                };

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

        public async Task<bool> ProcessRequestTrans01Async(TransunionRequest.ProcessRequestTrans01 Request, string UserID)
        {
            bool result = false;
            Logger log = new Logger();
            try
            {
                BureauEnquiry01 enquiry = new BureauEnquiry01
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    Address1Period = Request.Address1Period,
                    Address2City = Request.Address2City,
                    Address2Line1 = Request.Address2Line1,
                    Address2Line2 = Request.Address2Line2,
                    Address2Period = Request.Address2Period,
                    Address2PostalCode = Request.Address2PostalCode,
                    Address2ProvinceCode = Request.Address2ProvinceCode,
                    Address2Suburb = Request.Address2Suburb,
                    AddressLine1 = Request.AddressLine1,
                    AddressLine2 = Request.AddressLine2,
                    BankAccountNumber = Request.BankAccountNumber,
                    BankBranch = Request.BankBranch,
                    BankBranchCode = Request.BankBranchCode,
                    BankName = Request.BankName,
                    BirthDate = Request.BirthDate,
                    BranchNumber = Request.BankBranchCode,
                    CellNo = Request.CellNo,
                    City = Request.City,
                    EmailAddress = Request.EmailAddress,
                    Employer = Request.Employer,
                    EmploymentPeriod = Request.EmploymentPeriod,
                    EnquirerContactName = Request.EnquirerContactName,
                    EnquirerContactPhoneNo = Request.EnquirerContactPhoneNo,
                    EnquiryAmount = Request.EnquiryAmount,
                    EnquiryType = Request.EnquiryType,
                    Forename1 = Request.Forename1,
                    Forename2 = Request.Forename2,
                    Forename3 = Request.Forename3,
                    HomeTelCode = Request.HomeTelCode,
                    HomeTelNo = Request.HomeTelNo,
                    IdentityNo1 = Request.IdentityNo1,
                    IdentityNo2 = Request.IdentityNo2,
                    MaidenName = Request.MaidenName,
                    MaritalStatus = Request.MaritalStatus,
                    NoOfDependants = Request.NoOfDependants,
                    Occupation = Request.Occupation,
                    OperatorIdentity = Request.OperatorIdentity,
                    OwnerTenant = Request.OwnerTenant,
                    PostalCode = Request.PostalCode,
                    ProvinceCode = Request.ProvinceCode,
                    Salary = Request.Salary,
                    Sex = Request.Sex,
                    SpouseForename1 = Request.SpouseForename1,
                    SpouseForename2 = Request.SpouseForename2,
                    Suburb = Request.Suburb,
                    Surname = Request.Surname,
                    Title = Request.Title,
                    ul_long_score = Request.LongScore,
                    ul_medium_score = Request.MediumScore,
                    ul_short_score = Request.ShortScore,
                    WorkTelCode = Request.WorkTelCode,
                    WorkTelNo = Request.WorkTelNo

                };
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
    }
}
