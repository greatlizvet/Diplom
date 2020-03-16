using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Models;

namespace HomeForPets.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            Error error = (Error)RouteData.Values["ExceptionObject"];
            return View(error);
        }

        public ActionResult NotAvailable()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}