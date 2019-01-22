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

namespace DataSolution.Service.Controllers
{
    public class TransunionAPIController : ApiController , ITransunionAPI
    {
        ConsumerSoapClient client = new ConsumerSoapClient();
        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();

       
        public async Task<bool> BureauEnquiry37Async(string Enquirer, string EnquirerPhoneNo, string EnquiryType,string EnquiryAmount,string Surname,string Forename1,
                                                        string Forename2,string Forename3,string MaidenName,string DateOfBirth,string IdentityNo1, string IdentityNo2,
                                                        string Sex,string Title,string MaritalStatus,string Dependents,string AddressLine1, string AddressLine2, string Suburb,
                                                        string City,string HomePostalCode,string Province,string AddressPeriod,string OwnerTenant,string HomeTelCode,string HomeTelNo,
                                                        string WorkTelCode,string WorkTelNo,string SpouseForname1,string SpouseForname2,string PostalAddress1,string PostalAddress2,
                                                        string PostalSuburb,string PostalCode,string PostaProvinceCode,string Occupation,string Employer,string EmploymentPeriod,
                                                        string Salary,string BankName,string BankBranch,string BranchCode,string BankNo,string OperatorIdentity,string CellNo,
                                                        string Email,string[] InsuranceSubNo,string UserID)
        {

            bool result = false;
            try
            {
                    BureauEnquiry37 request = new BureauEnquiry37
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    EnquirerContactName = Enquirer,
                    EnquirerContactPhoneNo = EnquirerPhoneNo,
                    EnquiryType = EnquiryType,
                    EnquiryAmount = EnquiryAmount,
                    Surname = Surname,
                    Forename1 = Forename1,
                    Forename2 = Forename2,
                    MaidenName = MaidenName,
                    BirthDate = DateOfBirth,
                    IdentityNo1 = IdentityNo1,
                    IdentityNo2 = IdentityNo2,
                    Sex = Sex,
                    Title = Title,
                    MaritalStatus = MaritalStatus,
                    NoOfDependants = Dependents,
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    Suburb = Suburb,
                    City = City,
                    PostalCode = HomePostalCode,
                    ProvinceCode = Province,
                    Address1Period = AddressPeriod,
                    OwnerTenant = OwnerTenant,
                    HomeTelCode = HomeTelCode,
                    HomeTelNo = HomeTelNo,
                    WorkTelCode = WorkTelCode,
                    WorkTelNo = WorkTelNo,
                    SpouseForename1 = SpouseForname1,
                    SpouseForename2 = SpouseForname2,
                    Address2Line1 = PostalAddress1,
                    Address2Line2 = PostalAddress2,
                    Address2Suburb = PostalSuburb,
                    Address2ProvinceCode = PostaProvinceCode,
                    Occupation = Occupation,
                    Employer = Employer,
                    EmploymentPeriod = EmploymentPeriod,
                    Salary = Salary,
                    BankName = BankName,
                    BankBranch = BankBranch,
                    BankBranchCode = BranchCode,
                    BankAccountNumber = BankNo,
                    OperatorIdentity = OperatorIdentity,
                    CellNo = CellNo,
                    EmailAddress = Email,
                    InsuranceSubNo = InsuranceSubNo
                };



      
                var response = await client.ProcessRequestTrans37Async(request, Destination.Test);
               //Save the transaction
                result = true;
            }
            //catch (SoapException soapEX)
            //{
            //    Logger.LogError(UserID, "DataSolutions.Services", "BureauEnquiry37Async", soapEX.Message);

            //}
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.LogError(UserID, "DataSolutions.Services", "BureauEnquiry37Async", ex.Message);
            }
           

            return result;
        }

        
        public async Task<bool> IndividualTraceSearchAsync(string UserID,string TicketNo,string ReportNo,string SearchIndicator,string SearchType,string SearchReason,string SearchUserID,
                                                         string UserBranch,string Username,string UserSurname,string UserPhone,string Email,string ConsumerNo,string IDNo,string Forename,
                                                         string Surname,string PhoneCode,string TelNo,string CellNo,string DateOfBirth,string FromAge,string ToAge,string Gender,
                                                         string EmailAddress,string Employer,string StreetAddress,string AddressLine1, string AddressLine2,string Suburb,string Town,
                                                         string PostalCode,string Counter,string MoreCount,string RepeatDetail)
        {

            bool result = false;
            try
            {
                IndividualTraceSearchInput individualTrace = new IndividualTraceSearchInput
                {
                    SubscriberCode = subNo,
                    SecurityCode = securityCode,
                    TicketNumber = TicketNo,
                    ReportNumber = ReportNo,
                    SearchIndicator = SearchIndicator,
                    SearchType = SearchType,
                    SearchReason = SearchReason,
                    UserID = SearchUserID,
                    UserBranch = UserBranch,
                    UserName = Username,
                    UserSurname = UserSurname,
                    UserPhone = UserPhone,
                    UserEmail = Email,
                    ConsumerNumber = ConsumerNo,
                    IdentityNumber = IDNo,
                    Forename = Forename,
                    Surname = Surname,
                    TelephoneAreaCode = PhoneCode,
                    TelephoneNumber = TelNo,
                    CellNumber = CellNo,
                    DateOfBirth = DateOfBirth,
                    FromAge = FromAge,
                    ToAge = ToAge,
                    Gender = Gender,
                    EmailAddress = EmailAddress,
                    Employer = Employer,
                    AddressStreetNumber = StreetAddress,
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    AddressSuburb = Suburb,
                    AddressTown = Town,
                    AddressPostalCode = PostalCode,
                    Counter = Counter,
                    MoreCount = MoreCount,
                    RepeatDetail = RepeatDetail,
                    
                };

                var response = await client.ProcessRequestIndividualTraceSearchInputAsync(individualTrace);

                //Save Transaction

                result = true;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.LogError(UserID, "DataSolutions.Services", "IndividualTraceSearchAsync", ex.Message);
            }

            return result;

        }

        public async Task<bool> TraceOrder68Async(string UserID,string TicketNo,string ReportNo,string ConsumerNo,List<string> ProductCode,string IDNo1,string IDNo2,string TelCode1,
                                                    string TelNumber1,string TelCode2,string TelNumber2,string CellNo,string AddressLine1, string AddressLine2,string Suburb,string Code,string Province)
        {
            bool result = false;
            ModuleProductCode code = new ModuleProductCode();
            int count = ProductCode.Count;
            ModuleProductCode[] productCode = new ModuleProductCode[count];

            int i = 0;
            foreach (string  item in ProductCode)
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
                    TicketNo = TicketNo,
                    ReportNo = ReportNo,
                    ConsumerNumber = ConsumerNo,
                    ModuleProducts = productCode,
                    IDNo1 = IDNo1,
                    IDNo2 = IDNo2,
                    TelephoneCode1 = TelCode1,
                    TelephoneNumber1 = TelNumber1,
                    TelephoneCode2 = TelCode2,
                    TelephoneNumber2 = TelNumber2,
                    CellNo = CellNo,
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    Suburb = Suburb,
                    PostalCode = Code,
                    Province = Province
                };

                var response = await client.ProcessRequestIndividualTraceProductOrderTrans05Async(trace);
                
                //Save Transaction
                result = true;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.LogError(UserID, "DataSolutions.Services", "TraceOrder68Async", ex.Message);
            }


            return result;

        }
    }
}
