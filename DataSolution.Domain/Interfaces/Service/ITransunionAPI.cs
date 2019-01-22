using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Service
{
    public interface ITransunionAPI
    {
        Task<bool> BureauEnquiry37Async(string Enquirer, string EnquirerPhoneNo, string EnquiryType, string EnquiryAmount, string Surname, string Forename1,
                                                        string Forename2, string Forename3, string MaidenName, string DateOfBirth, string IdentityNo1, string IdentityNo2,
                                                        string Sex, string Title, string MaritalStatus, string Dependents, string AddressLine1, string AddressLine2, string Suburb,
                                                        string City, string HomePostalCode, string Province, string AddressPeriod, string OwnerTenant, string HomeTelCode, string HomeTelNo,
                                                        string WorkTelCode, string WorkTelNo, string SpouseForname1, string SpouseForname2, string PostalAddress1, string PostalAddress2,
                                                        string PostalSuburb, string PostalCode, string PostaProvinceCode, string Occupation, string Employer, string EmploymentPeriod,
                                                        string Salary, string BankName, string BankBranch, string BranchCode, string BankNo, string OperatorIdentity, string CellNo,
                                                        string Email, string[] InsuranceSubNo, string UserID);

        Task<bool> IndividualTraceSearchAsync(string UserID, string TicketNo, string ReportNo, string SearchIndicator, string SearchType, string SearchReason, string SearchUserID,
                                                         string UserBranch, string Username, string UserSurname, string UserPhone, string Email, string ConsumerNo, string IDNo, string Forename,
                                                         string Surname, string PhoneCode, string TelNo, string CellNo, string DateOfBirth, string FromAge, string ToAge, string Gender,
                                                         string EmailAddress, string Employer, string StreetAddress, string AddressLine1, string AddressLine2, string Suburb, string Town,
                                                         string PostalCode, string Counter, string MoreCount, string RepeatDetail);

        Task<bool> TraceOrder68Async(string UserID, string TicketNo, string ReportNo, string ConsumerNo, List<string> ProductCode, string IDNo1, string IDNo2, string TelCode1,
                                                   string TelNumber1, string TelCode2, string TelNumber2, string CellNo, string AddressLine1, string AddressLine2, string Suburb, string Code, string Province);

    }
}
