using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Interfaces.Utilities.Passwords;
using System.Security;
using System.Web.Security;

namespace DataSolution.Utilities.Passwords
{
    public class Password : IPassword
    {
        public string GenerateTempPassword()
        {
            return Membership.GeneratePassword(12, 1); 

        }
    }
}
