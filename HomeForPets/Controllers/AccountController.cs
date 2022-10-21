using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using HomeForPets.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using ModelDB;
using System.Linq;
using System.Collections.Generic;

namespace HomeForPets.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        OrderForRegistrationViewModel viewModel = new OrderForRegistrationViewModel();

        public AccountController()
        {
            List<City> cities = db.Cities.OrderBy(c => c.Name).ToList();
            viewModel.Cities = new SelectList(cities, "CityID", "Name");
        }
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/Home/Index")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                AppUser user = UserManager.Find(details.UserName, details.Password);

                if(user == null)
                {
                    ModelState.AddModelError("", "Некорректное имя или пароль");
                }
                else
                {
                    ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, identity);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        returnUrl = "/Home/Index";
                    }

                    return Redirect(returnUrl);
                }

                return View(details);
            }

            return View(details);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            AppUser user = UserManager.FindById(id);

            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult UsersForms()
        {
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult OrderForRegistration()
        {
            List<City> cities = db.Cities.OrderBy(c => c.Name).ToList();
            viewModel.Cities = new SelectList(cities, "CityID", "Name");

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult OrderForRegistration(OrderForRegistrationViewModel model, HttpPostedFileBase imageAvatar)
        {
            Image image = ImageService.SaveAvatar(imageAvatar);
            
            if (ModelState.IsValid)
            {
                db.Images.Add(image);

                model.OrderForRegistration.Image = image;
                db.OrderForRegistrations.Add(model.OrderForRegistration);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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