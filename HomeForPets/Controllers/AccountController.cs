using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using HomeForPets.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using ModelDB;

namespace HomeForPets.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        PetsDbContext db = new PetsDbContext();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/Home/Index")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel details, string returnUrl = "/Home/Index")
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
        [AllowAnonymous]
        public ActionResult OrderForRegistration()
        {
            OrderForRegistration viewModel = new OrderForRegistration();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult OrderForRegistration(OrderForRegistration model, HttpPostedFileBase imageAvatar)
        {
            Image image = ImageService.SaveAvatar(imageAvatar);

            if (ModelState.IsValid)
            {
                db.Images.Add(image);

                model.Image = image;
                db.OrderForRegistrations.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
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