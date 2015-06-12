using System.Web.Mvc;

namespace SMA._1._1.Areas.SMAAdministrator
{
    public class SMAAdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SMAAdministrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SMAAdministrator_default",
                "SMAAdministrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}