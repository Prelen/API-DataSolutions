using DataSolution.Data.DAL;
using DataSolution.Domain.Model.Data;
using DataSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static DataSolution.Domain.Model.Services.TransunionRequest;


namespace DataSolution.Areas.Products.Controllers
{
    public class ProductController : Controller
    {
        // GET: Products/Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestProduct()
        {
            if (Session["User"] is UserModel user)
            {
                return View();
            }
            else
                return RedirectToAction("Login","User", new { area = "Users"});

                
        }


        public ActionResult PersonalProducts()
        {
            if (Session["User"] is UserModel user)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { area = "Users" });
        }

        public async Task<JsonResult> GetConsumerProfile(string FirstName,string Surname,string IDNumber,string ProductID)
        {

            bool result = false;
            UserModel user = Session["User"] as UserModel;

            string userID = user.UserID.ToString();
           

            RequestTrans01 request = new RequestTrans01 {
                Forename1 = FirstName,
                Surname = Surname,
                IdentityNo1 = IDNumber
            };


             result = await new TransUnionConsumer().GetConsumerProfile(request, userID, Convert.ToInt32(ProductID));
            
       
            var audit = new AuditModel
            {
                ActivityDescription = "You requested a Consumer Profile report.",
                AuditDate = DateTime.Now,
                UserID = 4
            };

            result = new AuditData().InsertAudit(audit);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<bool> GetConsumerProfileWithAddress(string FirstName, string Surname, string IDNumber, string ProductID,string AddressLin1, string AddressLine2,
            string Suburb,string City,string ProvinceCode)
        {
            bool result = false;
            UserModel user = Session["User"] as UserModel;
            string userID = user.UserID.ToString();
            BureauEnquiry37Request request = new BureauEnquiry37Request
            {
                Forename1 = FirstName,
                Surname = Surname,
                IdentityNo1 = IDNumber,
                AddressLine1 = AddressLin1,
                AddressLine2 = AddressLine2,
                Suburb = Suburb,
                City = City,
                Province = ProvinceCode
            };

            result = await new TransUnionConsumer().GetConsumerProfileWithAddress(request, userID, Convert.ToInt32(ProductID));

            var audit = new AuditModel
            {
                ActivityDescription = "You requested a Consumer Profile report (with Address).",
                AuditDate = DateTime.Now,
                UserID = 4
            };

            result = new AuditData().InsertAudit(audit);

            return result;
        }

        public async Task<bool> PersonalTraceOrder(string IDNumber,string ProductID)
        {
            bool result = false;
            UserModel user = Session["User"] as UserModel;
            string userID = user.UserID.ToString();
            TraceOrder68Request request = new TraceOrder68Request
            {
                IDNo1 = IDNumber
            };

            result = await new TransUnionConsumer().PersonalTraceOrder(request, userID, Convert.ToInt32(ProductID));
            var audit = new AuditModel
            {
                ActivityDescription = "You requested a Personal Trace report.",
                AuditDate = DateTime.Now,
                UserID = 4
            };

            result = new AuditData().InsertAudit(audit);
            return result;
        }

        public JsonResult GetConsumerProducts()
        {
            var result = new ProductData().GetProductsByType(1);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinces()
        {
            var provinces = new LookupData().GetAllProvinces();
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }
    }
}