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
        public ViewResult Index(FormListViewModel model, int page = 1)
        {
            IQueryable<Form> unpublicForms = db.Forms.Where(f => f.UnPublished == true);
            IQueryable<Form> forms = db.Forms.Except(unpublicForms);

            int pageSize = 3;

            if (model != null)
            {
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
            }

            forms = SortForms(forms, model.Sort);

            IEnumerable<Form> formsPerPages = forms
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            formList.PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize,  TotalItems = forms.Count() };
            formList.Forms = formsPerPages;

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

        private IQueryable<Form> SortForms(IQueryable<Form> forms, string sortType)
        {
            IQueryable<Form> sortedForms = forms;

            switch(sortType)
            {
                case "asc":
                    sortedForms = forms.OrderBy(f => f.CreateDate);
                    break;
                default:
                    sortedForms = forms.OrderByDescending(f => f.CreateDate);
                    break;
            }

            return sortedForms;
        }
    }
}