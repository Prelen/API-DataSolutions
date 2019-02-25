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
    }
}
