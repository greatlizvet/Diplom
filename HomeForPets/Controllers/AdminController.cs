using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using HomeForPets.Models;
using Microsoft.AspNet.Identity;

namespace HomeForPets.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel model, HttpPostedFileBase file)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Image = ImageService.SaveAvatar(file)
                };

                IdentityResult result = UserManager.Create(user, model.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}