using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataSolution.Domain.Interfaces.Utilities.Mail;
using DataSolution.Domain.Model.Utilities;
using DataSolution.Domain.Model.Web.External;
using DataSolution.Utilities.Mail;

namespace DataSolution.Areas.External.Controllers
{
    public class ExternalController : Controller
    {
        // GET: External/External
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Home()
        {
            ViewBag.Title = "Home";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Title = "Our Services";
            return View();
        }

        public ActionResult ContactUs()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SendContactEmail(string FirstName,string Surname,string EmailAddress, string ContactNo,string Message)
        {
            //Get Template
            string path = Server.MapPath("/Templates");
            bool result = true;
            try
            {
                StringBuilder sb = new StringBuilder(System.IO.File.ReadAllText(path + @"/EmailTemplate.html"));
                sb.Replace("!!!FirstName!!!", FirstName)
                    .Replace("!!!Surname!!!", Surname)
                    .Replace("!!!Email!!!", EmailAddress)
                    .Replace("!!!ContactNo!!!", ContactNo)
                    .Replace("!!!Message!!!", Message);

                //Send email
                Domain.Model.Utilities.Email email = new Domain.Model.Utilities.Email
                {
                    Attachment = null,
                    EmailMessage = sb.ToString(),
                    FromAddress = "contactus@i-do-it.co.za",
                    HasAttachment = false,
                    Subject = "New Information Request",
                    ToEmail = new List<string> { "a83c6dabb1-f8b9c3@inbox.mailtrap.io" }
                };

               
               
                result = new Utilities.Mail.Email().SendEmail(email);
            }
            catch (Exception ex)
            {

                string str = ex.Message;
            }


            return Json(result);
        }

       
    }
}