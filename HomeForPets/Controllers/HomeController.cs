using System;
using System.Collections.Generic;
using System.Linq;
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
        public ViewResult Index(FormListViewModel model)
        {
            IQueryable<Form> forms = db.Forms;

            if (model != null)
            {
                forms = SortForms(forms, model.Sort);

                if(model.CategoryID != null && model.CategoryID != 0)
                {
                    forms = forms.Where(f => f.CategoryID == model.CategoryID);
                }

                if(model.SpecieID != null && model.SpecieID != 0)
                {
                    forms = forms.Where(f => f.SpecieID == model.SpecieID);
                }

                if(!String.IsNullOrEmpty(model.Search))
                {
                    forms = forms.Where(f => f.FormName.Contains(model.Search));
                }

                formList.Forms = forms.ToList();
            }

            return View(formList);
        }

        public JsonResult GetSpecies(int id)
        {
            List<Specie> species = db.Species.Where(s => s.CategoryID == id).ToList();
            formList.Species = new SelectList(species, "SpecieID", "SpecieName");

            return Json(formList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialAnketa()
        {
            return PartialView();
        }

        private IQueryable<Form> SortForms(IQueryable<Form> forms, SortType sortType)
        {
            switch(sortType)
            {
                case SortType.DateAsc:
                    forms.OrderBy(f => f.CreateDate);
                    return forms;
                case SortType.DateDesc:
                    forms.OrderByDescending(f => f.CreateDate);
                    return forms;
                default:
                    forms.OrderBy(f => f.CreateDate);
                    return forms;
            }
        }
    }
}