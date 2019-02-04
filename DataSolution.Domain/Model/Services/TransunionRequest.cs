using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Services
{
    public class TransunionRequest
    {
        public class BureauEnquiry37Request
        {
            public string Enquirer { get; set; }
            public string EnquirerPhoneNo { get; set; }
            public string EnquiryType { get; set; }
            public string EnquiryAmount { get; set; }
            public string Surname { get; set; }
            public string Forename1 { get; set; }
            public string Forename2 { get; set; }
            public string Forename3 { get; set; }
            public string MaidenName { get; set; }
            public string DateOfBirth { get; set; }
            public string IdentityNo1 { get; set; }
            public string IdentityNo2 { get; set; }
            public string Sex { get; set; }
            public string Title { get; set; }
            public string MaritalStatus { get; set; }
            public string Dependents { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string HomePostalCode { get; set; }
            public string Province { get; set; }
            public string AddressPeriod { get; set; }
            public string OwnerTenant { get; set; }
            public string HomeTelCode { get; set; }
            public string HomeTelNo { get; set; }
            public string WorkTelCode { get; set; }
            public string WorkTelNo { get; set; }
            public string SpouseForname1 { get; set; }
            public string SpouseForname2 { get; set; }
            public string PostalAddress1 { get; set; }
            public string PostalAddress2 { get; set; }
            public string PostalSuburb { get; set; }
            public string PostalCode { get; set; }
            public string PostaProvinceCode { get; set; }
            public string Occupation { get; set; }
            public string Employer { get; set; }
            public string EmploymentPeriod { get; set; }
            public string Salary { get; set; }
            public string BankName { get; set; }
            public string BankBranch { get; set; }
            public string BranchCode { get; set; }
            public string BankNo { get; set; }
            public string OperatorIdentity { get; set; }
            public string CellNo { get; set; }
            public string Email { get; set; }
            public string[] InsuranceSubNo { get; set; }
        }

        public class IndividualTraceSearchRequest
        {
            public string TicketNo { get; set; }
            public string ReportNo { get; set; }
            public string SearchIndicator { get; set; }
            public string SearchType { get; set; }
            public string SearchReason { get; set; }
            public string SearchUserID { get; set; }
            public string UserBranch { get; set; }
            public string Username { get; set; }
            public string UserSurname { get; set; }
            public string UserPhone { get; set; }
            public string Email { get; set; }
            public string ConsumerNo { get; set; }
            public string IDNo { get; set; }
            public string Forename { get; set; }
            public string Surname { get; set; }
            public string PhoneCode { get; set; }
            public string TelNo { get; set; }
            public string CellNo { get; set; }
            public string DateOfBirth { get; set; }
            public string FromAge { get; set; }
            public string ToAge { get; set; }
            public string Gender { get; set; }
            public string EmailAddress { get; set; }
            public string Employer { get; set; }
            public string StreetAddress { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Suburb { get; set; }
            public string Town { get; set; }
            public string PostalCode { get; set; }
            public string Counter { get; set; }
            public string MoreCount { get; set; }
            public string RepeatDetail { get; set; }
        }

        public class TraceOrder68Request
        {

            public string TicketNo { get; set; }
            public string ReportNo { get; set; }
            public string ConsumerNo { get; set; }
            public List<string> ProductCode { get; set; }
            public string IDNo1 { get; set; }
            public string IDNo2 { get; set; }
            public string TelCode1 { get; set; }
            public string TelNumber1 { get; set; }
            public string TelCode2 { get; set; }
            public string TelNumber2 { get; set; }
            public string CellNo { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Suburb { get; set; }
            public string Code { get; set; }
            public string Province { get; set; }
        }

        public class PayrollEmployerInformationRequest
        {
            public int PageIndex { get; set; }
            public int EmployerID { get; set; }
            public string CompanyName { get; set; }
            public string ProductCode { get; set; }
            public string PayslipCategory { get; set; }
            public bool IsLiteVersion { get; set; }
            public string BatchNumber { get; set; }
            public string BranchNumber { get; set; }
            public string ClientReference { get; set; }
            public string ClientReferenceID { get; set; }
        }

        public class PayslipInformationRequest
        {
            public string ContactName { get; set; }
            public string ContactNumber { get; set; }
            public string IdentityNumber { get; set; }
            public string Surname { get; set; }
            public string FirstName { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string AddressLine4 { get; set; }
            public string PostalCode { get; set; }
            public string EmployerDetailsID { get; set; }
            public string CompanyName { get; set; }
            public string BatchNumner { get; set; }
            public string BranchNumer { get; set; }
            public string CountryOfOrigin { get; set; }
            public string TransactionItemID { get; set; }
            public string PayslipCategory { get; set; }
            public bool IsLiteVersion
            {
                get; set;
            }
        }

        public class RequestTrans01
        {
            public string EnquirerContactName { get; set; }
            public string EnquirerContactPhoneNo { get; set; }
            public string EnquiryAmount { get; set; }
            public string EnquiryType { get; set; }
            public string Surname { get; set; }
            public string Forename1 { get; set; }
            public string Forename2 { get; set; }
            public string Forename3 { get; set; }
            public string MaidenName { get; set; }
            public string BirthDate { get; set; }
            public string IdentityNo1 { get; set; }
            public string IdentityNo2 { get; set; }
            public string Sex { get; set; }
            public string Title { get; set; }
            public string MaritalStatus { get; set; }
            public string NoOfDependants { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string ProvinceCode { get; set; }
            public string Address1Period { get; set; }
            public string OwnerTenant { get; set; }
            public string HomeTelCode { get; set; }
            public string HomeTelNo { get; set; }
            public string WorkTelCode { get; set; }
            public string WorkTelNo { get; set; }
            public string SpouseForename1 { get; set; }
            public string SpouseForename2 { get; set; }
            public string Address2Line1 { get; set; }
            public string Address2Line2 { get; set; }
            public string Address2Suburb { get; set; }
            public string Address2City { get; set; }
            public string Address2PostalCode { get; set; }
            public string Address2ProvinceCode { get; set; }
            public string Address2Period { get; set; }
            public string Occupation { get; set; }
            public string Employer { get; set; }
            public string EmploymentPeriod { get; set; }
            public string Salary { get; set; }
            public string BankName { get; set; }
            public string BankBranch { get; set; }
            public string BankBranchCode { get; set; }
            public string BankAccountNumber { get; set; }
            public string OperatorIdentity { get; set; }
            public string ShortScore { get; set; }
            public string MediumScore { get; set; }
            public string LongScore { get; set; }
            public string CellNo { get; set; }
            public string EmailAddress { get; set; }
            public string AverageScore { get; set; }
        }

        public class RequestTrans07
        {
            public RequestTrans01 RequestTrans01 { get; set; }
            public string[] InsuranceNo { get; set; }
        }
     
    }
}
