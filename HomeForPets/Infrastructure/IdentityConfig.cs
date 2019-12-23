using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeForPets.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace HomeForPets
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(PetsDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login")
            });
        }
    }
}