using System.Web.Mvc;

namespace SoPhoto.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_SP_Pics",
                "Admin/SP_Pics/{action}/{id}",
                new { controller = "Pic", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_SP_Topic",
                "Admin/SP_Topic/{action}/{id}",
                new { controller = "Topic", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_SP_Banner",
                "Admin/SP_Banner/{action}/{id}",
                new { controller = "Banner", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_SP_AD",
                "Admin/SP_AD/{action}/{id}",
                new { controller = "AD", action = "List", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_SP_News",
                "Admin/SP_News/{action}/{id}",
                new { controller = "News", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Console", action = "Login", id = UrlParameter.Optional }
                //new[] { "SoPhoto.Areas.Admin" }
            );
        }
    }
}
