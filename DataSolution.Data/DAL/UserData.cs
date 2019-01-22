using System;
using System.Collections.Generic;
using System.Linq;
using DataSolution.Data.Data;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;
using DataSolution.Utilities.Encryption;
using DataSolution.Utilities.Logging;

namespace DataSolution.Data.DAL
{

    public class UserData : IUserRepository
    {

        private readonly Logger log = new Logger();
        
       public List<UserTypeModel> GetAllUserTypes()
        {
            using (UserEntities user = new UserEntities())
            {
                var types = from ty in user.UserTypes
                            select  new UserTypeModel
                            {
                                TypeID = ty.UserTypeID,
                                TypeDescription = ty.UserType1
                            };                 
                            
                return types.ToList();
            }

        }


        public List<UserModel> GetAllUsers(int? UserID = null)
        {
            using (UserEntities user = new UserEntities())
            {
                var usrs = from usr in user.Users
                           select new UserModel
                           {
                               UserID = usr.UserID,
                               Username = usr.Username,
                               Password = usr.Password,
                               OrganizationName = usr.OrganizationName,
                               UserTypeID = usr.UserTypeID,
                               CreatedDate = usr.DateCreated,
                               LastLogin = usr.LastLogIn,
                               LoginCount = usr.LoginCount,
                               IsLocked = usr.IsLocked,
                               MasterOrganization = usr.MasterOrganization
                           };
                if (UserID != null)
                    usrs = usrs.ToList().AsQueryable()
                           .Where(u => u.UserID == UserID);

                return usrs.ToList();
            }
        }

        public List<UserModel> SearchAllUsers(int? UserID = null,string Username = null ,string OrgName = null,int? UserTypeID = null, DateTime? StartDate = null,               
                                               DateTime? EndDate = null,bool? Locked = null, int? MasterOrgID = null)
        {
            DataEncryption encryption = new DataEncryption();
            using (UserEntities user = new UserEntities())
            {

                var usrs = from usr in user.Users
                           select new UserModel
                           {
                               UserID = usr.UserID,
                               Username = usr.Username,
                               Password = usr.Password,
                               OrganizationName = usr.OrganizationName,
                               UserTypeID = usr.UserTypeID,
                               CreatedDate = usr.DateCreated,
                               LastLogin = usr.LastLogIn,
                               LoginCount = usr.LoginCount,
                               IsLocked = usr.IsLocked,
                               MasterOrganization = usr.MasterOrganization
                           };

                if (UserID != null)
                    usrs = usrs.ToList().AsQueryable()
                               .Where(u => u.UserID == UserID);

                if (Username != null)
                {
                    string encUsername = encryption.Encrypt(Username);
                    usrs = usrs.ToList().AsQueryable()
                              .Where(u => u.Username.Contains(encUsername));
                }
                   

                if (OrgName != null)
                    usrs = usrs.ToList().AsQueryable()
                               .Where(u => u.OrganizationName.Contains(OrgName));

                if (UserTypeID != null)
                    usrs = usrs.ToList().AsQueryable()
                               .Where(u => u.UserTypeID == UserTypeID);

                if (StartDate != null && EndDate != null)
                    usrs = usrs.ToList().AsQueryable()
                               .Where(u => u.CreatedDate >= StartDate && u.CreatedDate <= EndDate);

                if (Locked != null)
                    usrs = usrs.ToList().AsQueryable()
                               .Where(u => u.IsLocked == Locked);

                if (MasterOrgID != null)
                    usrs = usrs.ToList().AsQueryable()
                               .Where(u => u.MasterOrganization == MasterOrgID);

                return usrs.ToList();
            }
        }

        public bool InsertUser(UserModel User)
        {
            bool result = false;
            UserEntities userEntities = new UserEntities();
            DataEncryption encryption = new DataEncryption();
            string encUsername = encryption.Encrypt(User.Username);
            string encPwd = encryption.Encrypt(User.Password);
            Data.User user = new Data.User
            {
                Username = encUsername,
                Password = encPwd,
                OrganizationName = User.OrganizationName,
                UserTypeID = (int)User.UserTypeID,
                IsLocked = (bool)User.IsLocked,
                MasterOrganization = User.MasterOrganization,
                DateCreated = DateTime.Now
            };

            try
            {
                userEntities.Users.Add(user);
                userEntities.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                string userID = User.MasterOrganization != null ? User.MasterOrganization.ToString() : "New registration";
               
                log.LogError(userID, "DataSolutions.Data", "InsertUser", ex.Message);
            }
            return result;
        }


        public bool UpdateUser(UserModel User)
        {
            bool result = false;
            DataEncryption encryption = new DataEncryption();
           
            
            try
            {
                using (UserEntities users = new UserEntities())
                {
                    var user = (from usr in users.Users
                                where usr.UserID == User.UserID
                                select usr)
                               .FirstOrDefault();

                    if (user != null)
                    {
                        if (User.Username != null)
                        {
                            string encUsername = encryption.Encrypt(User.Username);
                            user.Username = encUsername;
                        }
                            

                        if (User.Password != null)
                        {
                            string encPwd = encryption.Encrypt(User.Password);
                            user.Password = encPwd;
                        }

                        if (User.OrganizationName != null)
                            user.OrganizationName = User.OrganizationName;

                        if (User.UserTypeID != null)
                            user.UserTypeID = (int)User.UserTypeID;

                        if (User.IsLocked != null)
                            user.IsLocked = (bool)User.IsLocked;

                        if (User.MasterOrganization != null)
                            user.MasterOrganization = User.MasterOrganization;

                        if (User.LastLogin != null)
                            user.LastLogIn = User.LastLogin;

                        if(User.LastLogin != null)
                            user.LoginCount = User.LoginCount;

                        users.SaveChanges();
                    }
                               
                }
            }
            catch (Exception ex)
            {

                log.LogError(User.UserID.ToString(), "DataSolutions.Data", "UpdateUser", ex.Message);
            }
          

           return result;

        }
    }
}