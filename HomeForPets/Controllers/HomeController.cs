using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ModelDB;
using HomeForPets.Infrastructure;
using HomeForPets.Models;
using PagedList;

namespace HomeForPets.Controllers
{
    public class HomeController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();

        FormListViewModel formList = FormViewService.InitializeFormList();
        // GET: Home
        public ActionResult Index(FormListViewModel model, int? page)
        {
            IQueryable<Form> unpublicForms = db.Forms.Where(f => f.UnPublished == true);
            IQueryable<Form> disableForms = db.Forms.Where(f => f.Enable == false);
            IQueryable<Form> forms = db.Forms.Except(unpublicForms);
            forms = forms.Except(disableForms);

            int pageSize = 27;
            int pageNumber = (page ?? 1);

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

                if(model.CityID != null && model.CityID != 0)
                {
                    forms = forms.Where(f => f.AppUser.CityID == model.CityID);
                }

                if(!String.IsNullOrEmpty(model.Search))
                {
                    forms = forms.Where(f => f.FormName.Contains(model.Search));
                }
            }

            forms = FilterSex(forms, model.Sex);
            forms = SortForms(forms, model.Sort);

            formList.CurrentSort = model.Sort;
            formList.CurrentSearch = model.Search;
            formList.CurrentCategory = model.CategoryID;
            formList.CurrentSpecie = model.SpecieID;
            formList.CurrentSex = model.Sex;
            formList.CurrentCity = model.CityID;
            formList.Forms = forms.ToPagedList(pageNumber, pageSize);

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

        private IQueryable<Form> FilterSex(IQueryable<Form> forms, string sex)
        {
            IQueryable<Form> sortedForms = forms;

            if(!String.IsNullOrEmpty(sex) && sex != "all")
            {
                if(sex == "man")
                {
                    sortedForms = sortedForms.Where(sf => sf.Sex == "man");
                }
                else if(sex == "woman")
                {
                    sortedForms = sortedForms.Where(sf => sf.Sex == "woman");
                }
            }

            return sortedForms;
        }
    }
}