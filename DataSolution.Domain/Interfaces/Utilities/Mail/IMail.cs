using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Model.Utilities;

namespace DataSolution.Domain.Interfaces.Utilities.Mail
{
    public interface IMail
    {
        bool SendEmail(Email Email);
        bool SendEmailWithAttachment(Email Email);
    }
}
