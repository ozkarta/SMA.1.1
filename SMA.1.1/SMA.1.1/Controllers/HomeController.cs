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

            if (sessionPersister.userRole == null)
            {
                return View();
            }
            else
            {
                string view;
                string controller;
                if (!this.ControllerContext.RouteData.Values["controller"].ToString().StartsWith(sessionPersister.userRole))
                {
                    controller = sessionPersister.userRole + this.ControllerContext.RouteData.Values["controller"].ToString();
                    view = this.ControllerContext.RouteData.Values["action"].ToString();
                }
                else
                {
                    view = this.ControllerContext.RouteData.Values["action"].ToString();
                    controller = this.ControllerContext.RouteData.Values["controller"].ToString();
                }

                return RedirectToAction(view, controller, new { Area = sessionPersister.userRole });
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

        //__________________________________________________________________________
       
        public RedirectToRouteResult translate()
        {
            sessionPersister.currentLanguageTrial = Request["Language"];
            GlobalVariables.initVariables();
            if (sessionPersister.userRole == null)
            {
                return RedirectToAction(Request["currentView"], Request["controller"]);
            }
            else
            {
                string view;
                string controller;
                if(!Request["controller"].StartsWith (sessionPersister.userRole))
                {
                    controller= sessionPersister.userRole+Request["controller"];
                    view=Request["currentView"];
                }
                else
                {
                    view=Request["currentView"];
                    controller = Request["controller"];
                }
                
                return RedirectToAction(view,controller, new { Area = sessionPersister.userRole });
            }
        }

          
      
       
    }
}