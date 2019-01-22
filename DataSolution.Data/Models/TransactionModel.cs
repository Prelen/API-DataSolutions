using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Data.Models
{
    public class TransactionModel
    {
        public int TransID { get; set;}
        public int ProductID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsSuccessful { get; set; }
        public string ErrorMEssage { get; set; }
    }
}
