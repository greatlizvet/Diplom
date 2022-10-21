using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using ModelDB;
using Microsoft.AspNet.Identity;

namespace HomeForPets.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();

        public ActionResult Index()
        {
            List<OrderForRegistration> orders = db.OrderForRegistrations.ToList();
            
            return View(orders);
        }

        [HttpPost]
        public ActionResult Denied(int? id, string comment)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            OrderForRegistration order = db.OrderForRegistrations.Find(id);

            if(order == null)
            {
                return HttpNotFound();
            }

            order.Denied = true;
            db.SaveChanges();

            string adminId = User.Identity.GetUserId();
            AppUser admin = db.Users.Find(adminId);
            string[] messageBody = new string[1];
            messageBody[0] = comment;

            MailSender.Send(admin.Email, admin.UserName, order.Email, messageBody, MailType.Deny);

            return RedirectToAction("Index");
        }
    }
}