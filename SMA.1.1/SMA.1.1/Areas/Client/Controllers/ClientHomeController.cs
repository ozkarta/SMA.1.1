using SMA._1._1.CS.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMA._1._1.Areas.Client.Controllers
{
    public class ClientHomeController : myControler
    {
        // GET: Client/ClientHome
        public ActionResult Index()
        {
            return View();
        }
    }
}