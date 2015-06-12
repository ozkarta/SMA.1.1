using System.Web.Mvc;

namespace SMA._1._1.Areas.CompanyWorker
{
    public class CompanyWorkerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CompanyWorker";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CompanyWorker_default",
                "CompanyWorker/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}