using HomeForPets.Infrastructure;
using HomeForPets.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeForPets.Controllers
{
    public class RoleAdminController : Controller
    {
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Required]string roleName)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result = RoleManager.Create(new AppRole(roleName));

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(roleName);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            AppRole role = RoleManager.FindById(id);

            if(role == null)
            {
                return HttpNotFound();
            }

            string[] membersIDs = role.Users.Select(u => u.UserId).ToArray();

            IEnumerable<AppUser> members = UserManager.Users.Where(u => membersIDs.Any(member => member == u.Id));
            IEnumerable<AppUser> nonMembers = UserManager.Users.Except(members);

            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public ActionResult Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if(ModelState.IsValid)
            {
                foreach(string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = UserManager.AddToRole(userId, model.RoleName);

                    if(!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                foreach(string userId in model.IdsToDel ?? new string[] { })
                {
                    result = UserManager.RemoveFromRole(userId, model.RoleName);

                    if(!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                return RedirectToAction("Index");
            }

            return View("Error", new string[] { "Роль не найдена" });
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            AppRole role = RoleManager.FindById(id);

            if(role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            AppRole role = RoleManager.FindById(id);

            if(role == null)
            {
                return HttpNotFound();
            }

            IdentityResult result = RoleManager.Delete(role);

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Error");
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}