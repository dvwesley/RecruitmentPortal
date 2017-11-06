using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentPortal.Common;

namespace RecruitmentPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (HttpContext.Application["Theme"] == null)
                    HttpContext.Application["Theme"] = LoadTheme("Ubuntu");
                return View();
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string LoadTheme(string Theme)
        {
            string sLocation = "";
            if (Theme == "Ubuntu")
            {
                sLocation = "Ubuntu/bootstrap.css";
            }
            else if (Theme == "Slate")
            {
                sLocation = "Slate/bootstrap.css";
            }            
            HttpContext.Application["Theme"] = sLocation;
            return HttpContext.Application["Theme"].ToString();
        }
    }
}