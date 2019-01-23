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
                    log.LogError(UserID, "DataSolutions.Services", "BureauEnquiry37Async", response.ErrorCode + " " +  response.ErrorMessage);
                //Save the transaction

            }
            catch (Exception ex)
            {
                
                log.LogError(UserID, "DataSolutions.Services", "BureauEnquiry37Async", ex.Message);
            }
           

            return result;
        }

        
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
                    log.LogError(UserID, "DataSolutions.Services", "IndividualTraceSearchAsync", response.ErrorCode + " " + response.ErrorMessage);


                //Save Transaction

                result = true;
            }
            catch (Exception ex)
            {
                
                log.LogError(UserID, "DataSolutions.Services", "IndividualTraceSearchAsync", ex.Message);
            }

            return result;

        }

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
                    log.LogError(UserID, "DataSolutions.Services", "TraceOrder68Async", response.ErrorCode + " " + response.ErrorMessage);
                //Save Transaction
                result = true;
            }
            catch (Exception ex)
            {
                
                log.LogError(UserID, "DataSolutions.Services", "TraceOrder68Async", ex.Message);
            }


            return result;

        }
    }
}
