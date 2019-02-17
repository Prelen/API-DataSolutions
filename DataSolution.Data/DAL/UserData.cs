using System;
using System.Collections.Generic;
using System.Linq;
using DataSolution.Data.Data;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;
using DataSolution.Utilities.Encryption;
using DataSolution.Domain.Interfaces.Utilities.Passwords;
using DataSolution.Utilities.Passwords;
using DataSolution.Utilities.Logging;
using AutoMapper;
using System.Threading.Tasks;

namespace DataSolution.Data.DAL
{

    public class UserData : IUserRepository
    {

        private  Logger log = new Logger();
        bool result;


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
                    string encUsername = new DataEncryption().Encrypt(Username);
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

        public bool InsertUser(UserModel UserDetails)
        {
             result = false;
            UserEntities userEntities = new UserEntities();
          
            string encUsername = new DataEncryption().Encrypt(UserDetails.Username);
            string encPwd = new DataEncryption().Encrypt(UserDetails.Password);
            
            try
            {
                Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<UserModel, User>();
                    }
                    );

                var user = Mapper.Map<User>(UserDetails);
                user.Username = encUsername;
                user.Password = encPwd;
                user.DateCreated = DateTime.Now;
                user.IsTempPassword = false;
                user.IsLocked = true;
                userEntities.Users.Add(user);
                userEntities.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                string userID = UserDetails.MasterOrganization != null ? UserDetails.MasterOrganization.ToString() : "New registration";
               
                log.LogError(userID, "DataSolutions.Data", "InsertUser", ex.Message);
            }
            return result;
        }


        public bool UpdateUser(UserModel User,bool IsEncrypted)
        {
             result = false;
 
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
                        if (!IsEncrypted)
                        {
                            if (User.Username != null)
                            {
                                string encUsername = new DataEncryption().Encrypt(User.Username);
                                user.Username = encUsername;
                            }

                           

                        }

                        if (User.Password != null)
                            user.Password = IsEncrypted ? User.Password : new DataEncryption().Encrypt(User.Password);
                    


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

                        if (User.IsTempPassword != null)
                            user.IsTempPassword = User.IsTempPassword;

                        if (User.FirstName != null)
                            user.FirstName = User.FirstName;

                        if (User.Surname != null)
                            user.Surname = User.Surname;

                        users.SaveChanges();
                        result = true;
                    }
                               
                }
            }
            catch (Exception ex)
            {

                log.LogError(User.UserID.ToString(), "DataSolutions.Data", "UpdateUser", ex.Message);
            }
          

           return result;

        }

        public async Task<bool> CheckUsernameAsync(string Username)
        {
          

            try
            {
                using (UserEntities users = new UserEntities())
                {

                    
                    var user = (from u in users.Users
                                where u.Username.Trim() == Username.Trim()
                                select u).Count();

                    
                    result = user > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                log.LogError("N/A","DataSolutions.Data", "CheckUsername", ex.Message);
            }

            return result;
        }


        public bool CheckUsername(string Username)
        {


            try
            {
                using (UserEntities users = new UserEntities())
                {


                    var user = (from u in users.Users
                                where u.Username.Trim() == Username.Trim()
                                select u).Count();


                    result = user > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                log.LogError("N/A", "DataSolutions.Data", "CheckUsername", ex.Message);
            }

            return result;
        }

        public UserModel ResetPassword(string EmailAddress)
        {

            try
            {
                using (UserEntities users = new UserEntities())
                {
                    var user = (from u in users.Users
                                where u.Email == EmailAddress.Trim()
                                select u).FirstOrDefault();

                    if (user != null)
                    {
                        //Reset Password
                        user.Password = new DataEncryption().Encrypt(new Password().GenerateTempPassword());
                        user.IsTempPassword = true;


                        Mapper.Initialize(
                            cfg =>
                            {
                                cfg.CreateMap<User, UserModel>();
                            }
                            );

                        var updatedUser = Mapper.Map<UserModel>(user);
                        
                        result = UpdateUser(updatedUser,true);
                        if (result)
                        {
                            return updatedUser;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }

            }
            catch (Exception ex)
            {

                log.LogError(EmailAddress, "DataSolutions.Data", "ResetPassword", ex.Message);
            }
            return null;

        }

        public UserModel Login(string Username, string Password)
        {
            UserModel userInfo = new UserModel();
            string encUsername = new DataEncryption().Encrypt(Username.Trim());
            string encPassword = new DataEncryption().Encrypt(Password.Trim());
            try
            {
                using (UserEntities users =  new UserEntities())
                {
                    var user = (from u in users.Users
                                where u.Username == encUsername && u.Password == encPassword
                              select u).FirstOrDefault();

                    if (user != null)
                    {
                        Mapper.Initialize(
                            cfg =>
                            {
                                cfg.CreateMap<User, UserModel>();
                            }
                            );

                        userInfo = Mapper.Map<UserModel>(user);
                    }
                    else
                    {
                        //Invalid password
                        var checkUser = SearchAllUsers(null, Username.Trim());
                        if (checkUser != null)
                        {
                            var usr = checkUser.FirstOrDefault();
                            if (usr.LoginCount < 3)
                                usr.LoginCount += 1;
                            else
                                usr.IsLocked = true;

                            UpdateUser(usr, true);
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                log.LogError(Username, "DataSolutions.Data", "Login", ex.Message);
            }
            return userInfo;
        }
    }
}