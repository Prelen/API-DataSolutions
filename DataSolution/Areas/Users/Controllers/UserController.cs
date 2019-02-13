using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataSolution.Data;
using DataSolution.Data.DAL;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;

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
                    UserTypeID = 2
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
    }
}