using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}