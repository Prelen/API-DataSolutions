using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Data
{
    public class TransactionModel
    {
        public class TransactionData
        {
            public int TransID { get; set; }
            public int UserID { get; set; }
            public int ProductID { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool? IsSuccessful { get; set; }
            public string Message { get; set; }
        }

        public class TransactionView
        {
            public int TransID { get; set; }
            public string MainOrganization { get; set; }
            public string Username { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public bool? IsSuccessful { get; set; }
            public string Message { get; set; }
        }

        public class TransactionReport
        {
           public  int UserID { get; set; }
           public  DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class TransactionsDataPoints
        {
            public int TransactionCount { get; set; }
            public string TransactionDate { get; set; }
        }
        
    }


}
