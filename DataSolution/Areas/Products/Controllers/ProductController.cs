using DataSolution.Data.DAL;
using DataSolution.Domain.Model.Data;
using DataSolution.Domain.Model.Services;
using DataSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static DataSolution.Domain.Model.Services.TransUnionCommercialRequest;
using static DataSolution.Domain.Model.Services.TransunionRequest;
using RequestTrans01 = DataSolution.Domain.Model.Services.TransunionRequest.RequestTrans01;


namespace DataSolution.Areas.Products.Controllers
{
    public class ProductController : Controller
    {
        private bool result;
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

        [HttpPost]
        public async Task<JsonResult> GetConsumerProfile(string FirstName,string Surname,string IDNumber,string ProductID)
        {

             result = false;
            UserModel user = Session["User"] as UserModel;

            string userID = user.UserID.ToString();
           

            RequestTrans01 request = new RequestTrans01 {
                Forename1 = FirstName,
                Surname = Surname,
                IdentityNo1 = IDNumber
            };


             result = await new TransUnionConsumer().GetConsumerProfile(request, userID, Convert.ToInt32(ProductID));

            SaveAudit("You requested a Consumer Profile report.", user.UserID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<bool> GetConsumerProfileWithAddress(string FirstName, string Surname, string IDNumber, string ProductID,string AddressLin1, string AddressLine2,
            string Suburb,string City,string ProvinceCode)
        {
             result = false;
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

            SaveAudit("You requested a Consumer Profile report (with Address).", user.UserID);

            return result;
        }

        [HttpPost]
        public async Task<bool> PersonalTraceOrder(string FirstName, string Surname, string IDNumber, string ProductID, string AddressLine1, string AddressLine2,
            string Suburb,string DateOfBirth, string ContactNo)
        {
             result = false;
            UserModel user = Session["User"] as UserModel;
            string userID = user.UserID.ToString();
            TransunionRequest.IndividualTraceSearchRequest request = new TransunionRequest.IndividualTraceSearchRequest 
            {
                Forename = FirstName,
                Surname = Surname,
                IDNo = IDNumber,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                Suburb = Suburb,
                DateOfBirth = DateOfBirth,
                TelNo = ContactNo
                
            };

            result = await new TransUnionConsumer().IndividualTraceSearchAsync(request, userID, 2);
            SaveAudit("You requested a Personal Trace report.", user.UserID);

            return result;
        }

        public JsonResult GetConsumerProducts(int ProductType)
        {
            var result = new ProductData().GetProductsByType(ProductType);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinces()
        {
            var provinces = new LookupData().GetAllProvinces();
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommercialProducts()
        {
            if (Session["User"] is UserModel user)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { area = "Users" });
        }

        [HttpPost]
        public async Task<JsonResult> BMSAlert(string StarDate,string EndDate)
        {
             result = false;
           
            UserModel user = Session["User"] as UserModel;
            BMSAlertsRetrieveRequest request = new BMSAlertsRetrieveRequest
            {
                startDate = Convert.ToDateTime(StarDate),
                endDate = Convert.ToDateTime(EndDate),
                alertType = "BankCodes"
            };

            result = await new Services.TransUnionCommercialService().BMSRetrieveAlert(request, user.UserID, 2);

     
            SaveAudit("You requested a BMS Alert report.", user.UserID);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<bool> PersonalReport(string FirstName,string Surname,string IDNumber, string AddressLine1,string AddressLin2,string Suburb,string City,
            string Province)
        {
            result = false;

            UserModel user = Session["User"] as UserModel;
            TransunionRequest.RequestTrans01 trans01 = new RequestTrans01
            {
                Forename1 = FirstName,
                Surname = Surname,
                IdentityNo1 = IDNumber,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLin2,
                Suburb = Suburb,
                City = City,
                ProvinceCode = Province
            };

            result = await new TransUnionConsumer().ProcessRequestTrans41Async(trans01, user.UserID.ToString(), 2);

            SaveAudit("You requested a Personal report+.", user.UserID);
            return result;
        }

        private bool SaveAudit(string Description,int UserID)
        {
            result = false;
            var audit = new AuditModel
            {
                ActivityDescription = Description,
                AuditDate = DateTime.Now,
                UserID = UserID
            };

            result = new AuditData().InsertAudit(audit);

            return result;
        }
    }
}