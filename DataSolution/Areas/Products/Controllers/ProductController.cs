using DataSolution.Data.DAL;
using DataSolution.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        public async Task<JsonResult> GetTestProduct(string IDNumber)
        {
            string userid = "4";
            string product = "1";
            string apiUrl = $"http://localhost:51312/api/TransunionAPI/ProcessRequestTransC20Async/{IDNumber}/{userid}/{product}" ; 
            

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                   // var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                }


            }

            var audit = new AuditModel
            {
                ActivityDescription = "You requested Test Product.",
                AuditDate = DateTime.Now,
                UserID = 4
            };

            bool result = new AuditData().InsertAudit(audit);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}