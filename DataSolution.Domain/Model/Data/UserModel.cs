using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Data
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrganizationName { get; set; }
        public int? UserTypeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? LoginCount { get; set; }
        public bool? IsLocked { get; set; }
        public int? MasterOrganization { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string WorkNo { get; set; }
        public bool IsTempPassword { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
      
    }
}
