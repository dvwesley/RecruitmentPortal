﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentPortal.Models;

namespace RecruitmentPortal.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }        

        public ActionResult AppError()
        {            
            return View();
        }

        public ActionResult UserNotFound()
        {
            return View();
        }
	}
}