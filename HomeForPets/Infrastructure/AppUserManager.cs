using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeForPets.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ModelDB;

namespace HomeForPets.Infrastructure
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) 
            : base(store)
        { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            ProjectDbContext db = context.Get<ProjectDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            return manager;
        }
    }
}