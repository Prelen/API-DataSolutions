using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Utilities
{
    public class Email
    {
        public string FromAddress { get; set; }
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string EmailMessage { get; set; }
        public bool HasAttachment { get; set; }
        public List<string> Attachment { get; set; }
     }
}
