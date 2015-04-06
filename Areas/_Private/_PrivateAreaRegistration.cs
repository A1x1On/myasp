using System.Web.Mvc;

namespace MedCardWeb.Areas._Private
{
    public class _PrivateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_Private";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_Private_default",
                "_Private/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
