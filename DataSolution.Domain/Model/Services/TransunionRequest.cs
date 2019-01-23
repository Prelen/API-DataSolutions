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
            public  string FromAge { get; set; }
            public  string ToAge { get; set; }
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
    }
}
