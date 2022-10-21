using System;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using HomeForPets.Models;
using Microsoft.AspNet.Identity;
using ModelDB;
using System.Collections.Generic;
using System.Linq;

namespace HomeForPets.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        CreateUserViewModel viewModel = new CreateUserViewModel();

        public AdminController()
        {
            List<City> cities = db.Cities.ToList();
            viewModel.Cities = new SelectList(cities, "CityID", "Name");
        }

        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [HttpGet]
        public ActionResult ConfirmCreateUser(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            OrderForRegistration order = db.OrderForRegistrations.Find(id);

            if(order == null)
            {
                return HttpNotFound();
            }

            viewModel.OrderForRegistration = order;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCreateUser(CreateUserViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Image image = ImageService.SaveAvatar(file);
                    db.Images.Add(image);

                    model.OrderForRegistration.Image = image;
                }

                AppUser user = new AppUser
                {
                    TitleName = model.OrderForRegistration.OrganizationName,
                    UserName = model.OrderForRegistration.Email,
                    Email = model.OrderForRegistration.Email,
                    PhoneNumber = model.OrderForRegistration.PhoneNumber,
                    Image = model.OrderForRegistration.Image,
                    CityID = (int)model.OrderForRegistration.CityID
                };

                IdentityResult result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    OrderForRegistration order = 
                        db.OrderForRegistrations
                        .Find(model.OrderForRegistration.OrderForRegistrationID);
                    order.Denied = true;
                    db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    string adminId = User.Identity.GetUserId();
                    AppUser admin = UserManager.FindById(adminId);
                    string[] messageBody = new string[2];
                    messageBody[0] = user.UserName;
                    messageBody[1] = model.Password;

                    MailSender.Send(admin.Email, admin.UserName, user.Email, messageBody, MailType.Accept);

                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            AppUser appUser = UserManager.FindById(id);

            if(appUser != null)
            {
                return View(appUser);
            }

            return RedirectToAction("Index");
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