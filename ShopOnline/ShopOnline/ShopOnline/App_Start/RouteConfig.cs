using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PayMent",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "PayMent", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
          );
            routes.MapRoute(
                name: "Accsesories",
                url: "Accsesories",
                defaults: new { controller = "Home", action = "Accsesories", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
            name: "Details",
            url: "Details",
            defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
        );
            routes.MapRoute(
            name: "RiddingGear",
            url: "RiddingGear",
            defaults: new { controller = "Home", action = "RiddingGear", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
        );
            routes.MapRoute(
        name: "Brand",
        url: "Brand",
        defaults: new { controller = "Home", action = "Brand", id = UrlParameter.Optional },
        namespaces: new[] { "ShopOnline.Controllers" }
    );
            routes.MapRoute(
      name: "Helmets",
      url: "Helmets",
      defaults: new { controller = "Home", action = "Helmets", id = UrlParameter.Optional },
      namespaces: new[] { "ShopOnline.Controllers" }
  );
            routes.MapRoute(
             name: "PayMent Success",
             url: "hoan-thanh",
             defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
             namespaces: new[] { "ShopOnline.Controllers" }
         );

            routes.MapRoute(
            name: "Contact",
            url: "lien-he",
            defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
        );
            routes.MapRoute(
           name: "Content",
           url: "tin-tuc",
           defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
       );
            routes.MapRoute(
            name: "Register",
            url: "dang-ky",
            defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
           name: "Login",
           url: "dang-nhap",
           defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
           name: "Search",
           url: "tim-kiem",
           defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
          name: "Cart",
          url: "gio-hang",
          defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
          namespaces: new[] { "ShopOnline.Controllers" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );


        }
    }
}
