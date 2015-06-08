using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMA.CS;
using System.Diagnostics;
using SMA._1._1.CS.Authentication;
namespace SMA.Controllers
{
    public class HomeController : myControler
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
            sessionPersister.currentLanguageTrial = Request["Language"];
            GlobalVariables.initVariables();
    
            return RedirectToAction(Request["currentView"],Request["controller"]);
        }

          
      
       
    }
}