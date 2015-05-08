﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMA.CS;
using System.Diagnostics;
namespace SMA.Controllers
{
    public class HomeController : Controller
    {

        //_____________________MENUE__ACTIONS______________________________________________
        public ActionResult Index()
        {
            ViewBag.Title = "Ozzle Application";
            return View();
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

        //__________________________________________________________________________
       
        public RedirectToRouteResult translate()
        {
            GlobalVariables.currentLanguageTrial = Request["Language"];
            GlobalVariables.initVariables();
    
            return RedirectToAction(Request["currentView"],Request["controller"]);
        }

          
      
       
    }
}