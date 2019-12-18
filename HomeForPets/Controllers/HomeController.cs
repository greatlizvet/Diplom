using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelDB;
using HomeForPets.Infrastructure;
using HomeForPets.Models;

namespace HomeForPets.Controllers
{
    public class HomeController : Controller
    {
        PetsDbContext db = new PetsDbContext();

        FormListViewModel formList = FormViewService.InitializeFormList();
        // GET: Home
        public ActionResult Index(int? category, int? specie)
        {
            IQueryable<Form> forms = db.Forms;

            if(category != null && category != 0)
            {
                forms = forms.Where(f => f.CategoryID == category);
            }

            if(specie != null && specie != 0)
            {
                forms = forms.Where(f => f.SpecieID == specie);
            }

            formList.Forms = forms.ToList();

            return View(formList);
        }

        public ActionResult PartialAnketa()
        {
            return PartialView();
        }
    }
}