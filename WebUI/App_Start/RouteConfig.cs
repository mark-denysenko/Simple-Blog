using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PostInfo",
                url: "Post/{id}",
                defaults: new { controller = "Blog", action = "PostInfo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "FeedPosts",
                url: "Posts/{page}",
                defaults: new { controller = "Blog", action = "AllPosts", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Profile",
                url: "MyProfile",
                defaults: new { controller = "Account", action = "Profile"}
            );

            routes.MapRoute(
                name: "Greeting",
                url: "Welcome",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "DefaultError",
                url: "DefaultError",
                defaults: new {controller = "Home", action = "DefaultError"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
