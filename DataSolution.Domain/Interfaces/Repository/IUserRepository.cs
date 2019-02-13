using DataSolution.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Repository
{
   public  interface IUserRepository
    {
        List<UserTypeModel> GetAllUserTypes();
        List<UserModel> GetAllUsers(int? UserID = null);
        List<UserModel> SearchAllUsers(int? UserID = null, string Username = null, string OrgName = null, int? UserTypeID = null, DateTime? StartDate = null,
                                     DateTime? EndDate = null, bool? Locked = null, int? MasterOrgID = null);
        bool InsertUser(UserModel User);
        bool UpdateUser(UserModel User);
        Task<bool> CheckUsernameAsync(string Username);
        bool CheckUsername(string Username);
        UserModel ResetPassword(string EmailAddress);



    }
}
