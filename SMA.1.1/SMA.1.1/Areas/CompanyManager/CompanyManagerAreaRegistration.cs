using System.Web.Mvc;

namespace SMA._1._1.Areas.CompanyManager
{
    public class CompanyManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CompanyManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CompanyManager_default",
                "CompanyManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}