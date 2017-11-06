using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentPortal.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Partial/
        [ChildActionOnly]
        public ActionResult List()
        {
            return View();
        } 
	}
}