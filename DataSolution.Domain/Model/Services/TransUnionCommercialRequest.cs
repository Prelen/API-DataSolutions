using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Services
{
    public class TransUnionCommercialRequest
    {
        public class BMSAlertsRetrieveRequest
        {
            public string subscriberNumber { get; set; }
            public string securityCode { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string alertType { get; set; }
        }

        public class BusinessSearchRequest
        {
            public string SearchType { get; set; }
            public string SubjectName { get; set; }
            public string RegistrationNo { get; set; }
            public string ITNumber { get; set; }
            public string DunsNumber { get; set; }
            public string VatNumber { get; set; }
            public string BankAccount { get; set; }
            public string TradingNumber { get; set; }
        }

        public class MailboxRequest
        {
            public string TicketNumber { get; set; }
        }

        public class ModuleAvailabilityRequest
        {
            public string ITNumber { get; set; }
        }

        public class AcountVerificationRequest
        {
            public string AccountHolder { get; set; }
            public string AccountNumber { get; set; }
            public string AccountType { get; set; }
            public string BranchCode { get; set; }
            public string RegistrationNumber { get; set; }
        }

        public class BankCodeRequest
        {
            public string AccountHolder { get; set; }
            public string AccountNumber { get; set; }
            public string BankAbbreviation { get; set; }
            public string Branch { get; set; }
            public string BranchCode { get; set; }
            public string CreditAmount { get; set; }
            public string SpecialInstructions { get; set; }
            public string TermsGiven { get; set; }
        }

        public class TradeReferenceRequest
        {
            public string Branch { get; set; }
            public string ContactName { get; set; }
            public string TelephoneDialingCode { get; set; }
            public string TelephoneNumber { get; set; }
            public string TradeName { get; set; }
        }

        public class ModuleProduct
        {
            public string Code { get; set; }
        }

        public class ModuleInvestigateRequest
        {
            public List<ModuleProduct> ModuleProducts { get; set; }
            public string EnquiryReason { get; set; }
            public string EnquiryAmount { get; set; }
            public string Terms { get; set; }
            public string UserOnTicket { get; set; }
            public string ContactForename { get; set; }
            public string ContactSurname { get; set; }
            public string ContactPhoneCode { get; set; }
            public string ContactPhoneNo { get; set; }
            public string AdditionalClientReference { get; set; }
            public string SubjectPhoneCode { get; set; }
            public string SubjectPhoneNo { get; set; }
            public string SubjectNameOnTicket { get; set; }
            public string SubjectAddress { get; set; }
            public string SubjectSuburb { get; set; }
            public string SubjectCity { get; set; }
            public string SubjectPostCode { get; set; }
            public string InvestigateOption { get; set; }
            public List<AcountVerificationRequest> AcountVerifications { get; set; }
            public List<BankCodeRequest> BankCodes { get; set; }
            public List<TradeReferenceRequest> TradeReferences { get; set; }
        }

        public class PrincipalRequest
        {
            public string FirstName { get; set; }
            public string Surname { get; set; }
            public string Initials { get; set; }
            public string SAID { get; set; }
            public string BirthDate { get; set; }
            public string OtherID { get; set; }
            public string AddressLine { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string UserID { get; set; }
            public string ContactName { get; set; }
            public string ContactSurname { get; set; }
            public string PhoneCode { get; set; }
            public string PhoneNo { get; set; }
            public string Reference { get; set; }
            public string EnquiryReason { get; set; }
        }

        public class ProductAccountVerificationRequest
        {
            public string AccountHolder { get; set; }
            public string AccountNumber { get; set; }
            public string AccountType { get; set; }
            public string BranchCode { get; set; }
        }

        public class PrincipalModuleRequest
        {
            public string Surname { get; set; }
            public string FirstName { get; set; }
            public string Initials { get; set; }
            public string IDNumber { get; set; }
            public string OtherID { get; set; }
            public string BirthDate { get; set; }
            public string AddressLine { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string UserID { get; set; }
            public string ContactName { get; set; }
            public string ContactSurname { get; set; }
            public string PhoneCode { get; set; }
            public string PhoneNo { get; set; }
            public string Reference { get; set; }
            public string EnquiryReason { get; set; }
            public string EnquiryAmount { get; set; }
            public string BusinessName { get; set; }
            public string RegistrationNumber { get; set; }
            public string VATNumber { get; set; }
            public string Email { get; set; }
            public List<ModuleAvailabilityRequest> ModuleProducts { get; set; }
            public List<ProductAccountVerificationRequest> PrincipalAccountVerifications { get; set; }
        }

        public class RequestTrans01
        {
            public string SearchType { get; set; }
            public string SubjectName { get; set; }
            public string RegistrationNo { get; set; }
            public string ITNumber { get; set; }
            public string DunsNumber { get; set; }
            public string VATNumber { get; set; }
            public string BankAccountNumber { get; set; }
            public string TradingNumber { get; set; }
        }
        public class RequestTransaction
        {
            public string ITNumber { get;set;}
        }

        public class RequestTrans15
        {
            public string ITNumber { get; set; }
            public List<string> ModuleProductCodes { get; set; }
            public string EnquiryReason { get; set; }
            public string EnquiryAmount { get; set; }
            public string Terms { get; set; }
            public string UserOnTicket { get; set; }
            public string ContactForename { get; set; }
            public string ContactSurname { get; set; }
            public string ContactPhoneCode { get; set; }
            public string ContactPhoneNo { get; set; }
            public string AdditionalClientReference { get; set; }
            public string SubjectPhoneCode { get; set; }
            public string SubjectPhoneNo { get; set; }
            public string SubjectNameOnTicket { get; set; }
            public string SubjectAddress { get; set; }
            public string SubjectSuburb { get; set; }
            public string SubjectCity { get; set; }
            public string SubjectPostCode { get; set; }
            public string InvestigateOption { get; set; }
            public string BankAccountNo { get; set; }
            public string BankAbbreviation { get; set; }
            public string BankBranch { get; set; }
            public string BankBranchCode { get; set; }
            public string SpecialInstructions { get; set; }
            public string BankCreditAmount { get; set; }
            public string BankTermsGiven { get; set; }
            public string BankAccountHolder { get; set; }
        }

        public class RequestTrans21
        {
            public string NumberType { get; set; }
            public List<ModuleProduct> ModuleProducts { get; set; }
            public string EnquiryReason { get; set; }
            public string Amount { get; set; }
            public string Terms { get; set; }
            public string UserID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string PhoneCode { get; set; }
            public string PhoneNumber { get; set; }
            public string Reference { get; set; }
            public string SubjectPhoneCode { get; set; }
            public string SubjectPhoneNumber { get; set; }
            public string SubjectName { get; set; }
            public string SubjectAddressLine { get; set; }
            public string SubjectSuburb { get; set; }
            public string SubjectTown { get; set; }
            public string SubjectCode { get; set; }
            public string Investigate { get; set; }
            public string AccountNumber { get; set; }
            public string BankAbbreviation { get; set; }
            public string Branch { get; set; }
            public string BranchCode { get; set; }
            public string SpecialInstructions { get; set; }
            public string CreditAmount { get; set; }
            public string TermsGiven { get; set; }
            public string AccountHolder { get; set; }
        }

        public class PrincipalTransaction
        {
            public string Surname { get; set; }
            public string Forename { get; set; }
            public string Initials { get; set; }
            public string IDNumber { get; set; }
            public string OtherID { get; set; }
            public string DateOfBirth { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string ProvinceCode { get; set; }
            public string Gender { get; set; }
        }

        public class RequestTrans32
        {
            public string ContactName { get; set; }
            public string ContactSurname { get; set; }
            public string PhoneCode { get; set; }
            public string PhoneNo { get; set; }
            public string UserId { get; set; }
            public string BusinessName { get; set; }
            public string RegistrationNumber { get; set; }
            public string VATNumber { get; set; }
            public string TradingName { get; set; }
            public string CompanyType { get; set; }
            public string BranchCode { get; set; }
            public string EnquiryReason { get; set; }
            public string Module { get; set; }
            public List<PrincipalTransaction> Principals { get; set; }
        }

        public class ForensicRequest
        {
            public string Surname { get; set; }
            public string FirstName { get; set; }
            public string Initials { get; set; }
            public string IDNumber { get; set; }
            public string OtherIDNumber { get; set; }
            public string DateOfBirth { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string UserId { get; set; }
            public string ContactName { get; set; }
            public string ContactSurname { get; set; }
            public string PhoneNumber { get; set; }
            public string Reference { get; set; }
            public string EnquiryReason { get; set; }
            public string CreditAmount { get; set; }
            public string Email { get; set; }
        }

        public class CommercialModuleRequest
        {
            public string ITNumber { get; set; }
            public List<ModuleProduct> ModuleProductCodes;
            public string EnquiryReason { get; set; }
            public string EnquiryAmount { get; set; }
            public string Terms { get; set; }
            public string UserOnTicket { get; set; }
            public string ContactForename { get; set; }
            public string ContactSurname { get; set; }
            public string ContactPhoneCode { get; set; }
            public string ContactPhoneNo { get; set; }
            public string AdditionalClientReference { get; set; }
            public string SubjectPhoneCode { get; set; }
            public string SubjectPhoneNo { get; set; }
            public string SubjectNameOnTicket { get; set; }
            public string SubjectAddress { get; set; }
            public string SubjectSuburb { get; set; }
            public string SubjectCity { get; set; }
            public string SubjectPostCode { get; set; }
            public string InvestigateOption { get; set; }
            public string BankAccountNo { get; set; }
            public string BankAbbreviation { get; set; }
            public string BankBranch { get; set; }
            public string BankBranchCode { get; set; }
            public string SpecialInstructions { get; set; }
            public string BankCreditAmount { get; set; }
            public string BankTermsGiven { get; set; }
            public string BankAccountHolder { get; set; }
        }
    }
}
