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

       public class RequestTrans18
        {
            public string EnquirerContactName { get; set; }
            public string EnquirerContactPhoneNo { get; set; }
            public string EnquiryAmount { get; set; }
            public string EnquiryType { get; set; }
            public string TransactionFlag1 { get; set; }
            public string TransactionFlag2 { get; set; }
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
            public string NlrData { get; set; }
            public string CellNo { get; set; }
            public string EmailAddress { get; set; }
            public string ProductType { get; set; }
            public string Consent { get; set; }
            public string GrossIncome { get; set; }
            public string IncomeTax { get; set; }
            public string PensionFund { get; set; }
            public string UIF { get; set; }
            public string MinTakeHomePay { get; set; }
            public string MedicalAid { get; set; }
            public string Utilities { get; set; }
            public string Sundry { get; set; }
            public string PayRent { get; set; }
            public string OtherDebtsNonCCA { get; set; }
            public string BondValue { get; set; }
            public string SchoolFees { get; set; }
            public string AddPriorityPay1 { get; set; }
            public string AddPriorityPay1Desc { get; set; }
            public string AddPriorityPay2 { get; set; }
            public string AddPriorityPay2Desc { get; set; }
            public string AddPriorityPay3 { get; set; }
            public string AddPriorityPay3Desc { get; set; }
            public string AddPriorityPay4 { get; set; }
            public string AddPriorityPay4Desc { get; set; }
            public string AddPriorityPay5 { get; set; }
            public string AddPriorityPay5Desc { get; set; }
            public string ConfidenceLevelPerc { get; set; }
        }

        public class RequestTrans22
        {
            public string LoanRegistrationNo { get; set; }
            public string SupplierReferenceNo { get; set; }
            public string EnquiryReferenceNo { get; set; }
            public string ConsumerSurname { get; set; }
            public string Forename1 { get; set; }
            public string Forename2 { get; set; }
            public string Forename3 { get; set; }
            public string SpouseCurrentSurname { get; set; }
            public string SpouseForename1 { get; set; }
            public string SpouseForename2 { get; set; }
            public string ConsumerBirthDate { get; set; }
            public string ConsumerIdentityNumber { get; set; }
            public string CurrentAddressLine1 { get; set; }
            public string CurrentAddressLine2 { get; set; }
            public string CurrentAddressLine3 { get; set; }
            public string CurrentAddressLine4 { get; set; }
            public string CurrentAddressPostalCode { get; set; }
            public string PeriodCurrentAddress { get; set; }
            public string OtherAddressLine1 { get; set; }
            public string OtherAddressLine2 { get; set; }
            public string OtherAddressLine3 { get; set; }
            public string OtherAddressLine4 { get; set; }
            public string OtherAddressPostalCode { get; set; }
            public string OwnerTenantCurrentAddress { get; set; }
            public string ConsumerOccupation { get; set; }
            public string ConsumerEmployer { get; set; }
            public string ConsumerMaidenName { get; set; }
            public string ConsumerAliasName { get; set; }
            public string Gender { get; set; }
            public string ConsumerMaritalStatus { get; set; }
            public string ConsumerHomeTelephoneCode { get; set; }
            public string ConsumerHomeTelephoneNumber { get; set; }
            public string ConsumerWorkTelephoneCode { get; set; }
            public string ConsumerWorkTelephoneNumber { get; set; }
            public string ConsumerCellularNumber { get; set; }
            public string LoanAmount { get; set; }
            public string InstalmentAmount { get; set; }
            public string GrossMonthlySalary { get; set; }
            public string SalaryFrequency { get; set; }
            public string RepaymentPeriod { get; set; }
            public string EnquiryReason { get; set; }
            public string BranchCode { get; set; }
            public string AccountNumber { get; set; }
            public string SubAccountNumber { get; set; }
            public string LoanType { get; set; }
            public string DateLoanDisbursed { get; set; }
            public string LoanAmount2 { get; set; }
            public string LoanAmountBalanceInd { get; set; }
            public string CurrentBalance { get; set; }
            public string CurrentBalanceInd { get; set; }
            public string MonthlyInstallment { get; set; }
            public string LoadIndicator { get; set; }
            public string RepaymentPeriod2 { get; set; }
            public string LoanPurpose { get; set; }
            public string TotalAmountRepayable { get; set; }
            public string InterestType { get; set; }
            public string AnnualRateForTotalChargeOfCredit { get; set; }
            public string RandValueOfInterestCharges { get; set; }
            public string RandValueOfTotalChargeOfCredit { get; set; }
            public string SettlementAmount { get; set; }
            public string ReAdvanceIndicator { get; set; }
           
        }

        public class RequestTrans23
        {
            public string LoanRegistrationNo { get; set; }
            public string EnquiryReferenceNo { get; set; }
            public string BranchCode { get; set; }
            public string AccountNumber { get; set; }
            public string SubAccountNo { get; set; }
            public string IDNumber { get; set; }
            public string DateOfBirth { get; set; }
            public string Surname { get; set; }
            public string Forename1 { get; set; }
            public string Forename2 { get; set; }
            public string Forename3 { get; set; }
            public string StatusCode { get; set; }
            public string DateOfClosureOrCancellation { get; set; }
        }

        public class RequestC29
        {
            public string TraceDataSelection { get; set; }
            public string EnquiryReason { get; set; }
            public string IDNumber { get; set; }
            public string TelephoneType { get; set; }
            public string TelephoneNumber { get; set; }
            public string Surname { get; set; }
            public string DateOfBirth { get; set; }
            public string Address { get; set; }
            public string PostalCode { get; set; }
        }

        public class RequestTransC30
        {
            public string OriginalITCRequestID { get; set; }
            public string OriginalClientRequestID { get; set; }
            public string PossibleConsumerNumber { get; set; }
        }

        public class RequestTransC4
        {
            public int ConsumerNumber { get; set; }
            public string SubscriberReference { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Acton { get; set; }
        }

       

    }
}
