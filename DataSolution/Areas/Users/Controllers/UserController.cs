using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataSolution.Data;
using DataSolution.Data.DAL;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;
using DataSolution.Utilities.Encryption;

namespace DataSolution.Areas.Users.Controllers
{
    public class UserController : Controller
    {
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
            }
            catch (Exception ex)
            {

                throw;
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
            TempData["User"] = user;
            return Json(user);
        }

        public ActionResult ResetPassword()
        {
            return View();
        }


        public ActionResult Home()
        {
            if (TempData["User"] is UserModel user)
            {
                if (user.IsTempPassword)
                    RedirectToAction("ResetPassword");
            }
            else
                RedirectToAction("Login");

            return View();
        }
    }
}