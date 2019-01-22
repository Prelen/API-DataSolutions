using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Service
{
    public interface IPDFConverter
    {
         Task<bool> ConvertPDFAsync(string Username, string UserEmail, string TicketNo, string UserID);
    }
}
