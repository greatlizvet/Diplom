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

            List<Category> categories = db.Categories.ToList();
            List<Specie> species = db.Species.ToList();

            FormListViewModel formList = new FormListViewModel
            {
                Forms = forms.ToList(),
                Categories = new SelectList(categories, "CategoryID", "CategoryName"),
                Species = new SelectList(species, "SpecieID", "SpecieName")
            };

            return View(formList);
        }

        public ActionResult PartialAnketa()
        {
            return PartialView();
        }
    }
}