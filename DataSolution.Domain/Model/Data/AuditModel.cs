using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Data
{
    public class AuditModel
    {
        public int AuditID { get; set; }
        public int UserID { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime? AuditDate { get; set; }
    }
}
