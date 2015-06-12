using System.Web.Mvc;

namespace SMA._1._1.Areas.CompanyHR
{
    public class CompanyHRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CompanyHR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CompanyHR_default",
                "CompanyHR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}