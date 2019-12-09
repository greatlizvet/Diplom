using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelDB;
using HomeForPets.Infrastructure;

namespace HomeForPets.Controllers
{
    public class HomeController : Controller
    {
        PetsDbContext db = new PetsDbContext();
        // GET: Home
        public ActionResult Index()
        {
            List<Form> forms = db.Forms.ToList();
            return View(forms);
        }

        public ActionResult PartialAnketa()
        {
            return PartialView();
        }
    }
}