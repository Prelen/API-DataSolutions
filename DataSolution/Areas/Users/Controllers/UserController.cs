using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DataSolution.Data;
using DataSolution.Data.DAL;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;
using DataSolution.Domain.Model.Web.Charts;
using DataSolution.Utilities.Encryption;
using DataSolution.Utilities.Logging;

namespace DataSolution.Areas.Users.Controllers
{
    public class UserController : Controller
    {

        AuditModel auditModel;
        // GET: Users/User
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SaveUser(string FirstName, string Surname,string CompanyName,string EmailAddress,string Username,string Password,string ContactNo)
        {
            bool result = false;
            try
            {
                UserModel user = new UserModel
                {
                    CreatedDate = DateTime.Now,
                    Email = EmailAddress,
                    IsLocked = true,
                    MobileNo = ContactNo,
                    OrganizationName = CompanyName,
                    Password = Password,
                    Username = Username,
                    UserTypeID = 2,
                    FirstName = FirstName,
                    Surname = Surname
                };

                result = new UserData().InsertUser(user);
                //Save Audit Details
                auditModel = new AuditModel
                {
                    ActivityDescription = "Your account was created",
                    AuditDate = DateTime.Now,
                    UserID = user.UserID
                };

                result = new AuditData().InsertAudit(auditModel);
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError("0", "DataSolutions.Web", "SaveUser", ex.Message);
            }
           


            return Json(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> CheckIfUsernameExistsAsync(string Username)
        {

            UserData user = new UserData();
            bool result = await user.CheckUsernameAsync(Username);
            result = true;
            return Json(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckIfUsernameExists(string Username)
        {

            IUserRepository user = new UserData();
            bool result =  user.CheckUsername(Username);
            return Json(result);
        }

      
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult ResetPassword(string Email)
        {
            
            bool result = false;
            var user = new UserData().ResetPassword(Email);
            if (user != null)
            {
                //Read Template and send email
                string path = Server.MapPath("/Templates");
                StringBuilder sb = new StringBuilder(System.IO.File.ReadAllText(path + @"/ResetPassword.html"));
                sb.Replace("!!!FirstName!!!", user.FirstName)
                 .Replace("!!!Surname!!!", user.Surname)
                 .Replace("!!!Password!!!", new DataEncryption().Decrypt(user.Password));

                //Audit 
                result = new UserData().InsertUser(user);
                //Save Audit Details
                auditModel = new AuditModel
                {
                    ActivityDescription = "Your password was reset",
                    AuditDate = DateTime.Now,
                    UserID = user.UserID
                };

                result = new AuditData().InsertAudit(auditModel);

                //Test Email TODO: Remove 
                user.Email = "a83c6dabb1-f8b9c3@inbox.mailtrap.io";

                Domain.Model.Utilities.Email email = new Domain.Model.Utilities.Email
                {
                    Attachment = null,
                    EmailMessage = sb.ToString(),
                    FromAddress = "no-reply@i-do-it.co.za",
                    HasAttachment = false,
                    Subject = "Password Reset",
                    ToEmail = new List<string> { user.Email }

                };

                result = new Utilities.Mail.Email().SendEmail(email);
            }

            return Json(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(string Username, string Password)
        {
            bool result = false;
            IUserRepository repo = new UserData();
            
            var user = repo.Login(Username, Password);
            Session["User"] = user;
            return Json(user);
        }

        public ActionResult ResetPassword()
        {
            return View();
        }


        public ActionResult Home()
        {
            if (Session["User"] is UserModel user)
            {
                if (user.IsTempPassword)
                  return  RedirectToAction("ResetPassword");
                else
                {

                   
                    //Save Audit Details
                    auditModel = new AuditModel
                    {
                        ActivityDescription = "You successfully logged in",
                        AuditDate = DateTime.Now,
                        UserID = user.UserID
                    };

                    new AuditData().InsertAudit(auditModel);

                    return View(user);
                }
                    
            }
            else
               return RedirectToAction("Login");

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckTempPassword(string TempPassword)
        {
            bool result = false;
            if (Session["User"] is UserModel user)
            {
                IUserRepository userRepo = new UserData();
                result = userRepo.CheckTempPassword(user.UserID, TempPassword);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult PasswordReset(string NewPassword)
        {
            bool result = false;
            if (Session["User"] is UserModel user)
            {
                user.Password = new DataEncryption().Encrypt(NewPassword);
                user.IsTempPassword = false;
                result = new UserData().UpdateUser(user, true);
            }
            return Json(result);
        }

        public JsonResult GetChartValues()
        {
          
            ChartOptions[] options = new ChartOptions[10];
            options[0] = new ChartOptions { label = "20-01",value = 6 };
            options[1] = new ChartOptions { label = "21-01", value = 7 };
            options[2] = new ChartOptions { label = "22-01", value = 2};
            options[3] = new ChartOptions { label = "23-01", value = 9 };
            options[4] = new ChartOptions { label = "24-01", value = 4 };
            options[5] = new ChartOptions { label = "25-01", value = 7 };
            options[6] = new ChartOptions { label = "26-01", value = 12 };
            options[7] = new ChartOptions { label = "27-01", value = 5 };
            options[8] = new ChartOptions { label = "28-01", value = 9 };
            options[9] = new ChartOptions { label = "29-01", value = 3 };

            var json = new JavaScriptSerializer().Serialize(options);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

       

       
    }
}