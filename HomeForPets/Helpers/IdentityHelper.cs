using HomeForPets.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeForPets.Helpers
{
    public static class IdentityHelper
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            AppUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

            return new MvcHtmlString(manager.FindByIdAsync(id).Result.UserName);
        }
    }
}