using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using HomeForPets.Models;
using Microsoft.AspNet.Identity;
using ModelDB;

namespace HomeForPets.Controllers
{
    [Authorize(Roles = "Administrator")]
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

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            AppUser appUser = UserManager.FindById(id);

            if(appUser == null)
            {
                return HttpNotFound();
            }

            return View(appUser);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string Id)
        {
            AppUser user = UserManager.FindById(Id);

            if(user == null)
            {
                return HttpNotFound();
            }

            IdentityResult result = UserManager.Delete(user);

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Error");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            AppUser appUser = UserManager.FindById(id);

            if(appUser != null)
            {
                return View(appUser);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(AppUser user, HttpPostedFileBase file)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = UserManager.FindById(user.Id);

                appUser.UserName = user.UserName;
                appUser.Email = user.Email;
                appUser.PhoneNumber = user.PhoneNumber;
                appUser.PasswordHash = UserManager.PasswordHasher.HashPassword(user.PasswordHash);

                if(file != null)
                {
                    appUser.Image = ImageService.SaveAvatar(file);
                }

                IdentityResult result = UserManager.Update(appUser);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(user);
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