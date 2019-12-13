using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using ModelDB;

namespace HomeForPets.Controllers
{
    public class FormController : Controller
    {
        PetsDbContext db = new PetsDbContext();

        public void AddDefault()
        {
            db.Images.Add(new Image { Path = "/Content/Images/default.jpg" });
        }
        
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            Form form = db.Forms.Find(id);

            if(form == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(form);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Category> categories = db.Categories.ToList();
            List<Specie> species = db.Species.ToList();

            FormCreateViewModel formCreate = new FormCreateViewModel
            {
                Categories = new SelectList(categories, "CategoryID", "CategoryName"),
                Specie = new SelectList(species, "SpecieID", "SpecieName")
            };

            return View(formCreate);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Form form, HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();

            if(ModelState.IsValid)
            {
                form.CreateDate = DateTime.Now;
                //Временная заглушка
                form.ProfileID = 1;

                images = form.SaveImage(files);

                foreach(var img in images)
                {
                    db.Images.Add(img);
                }

                foreach (var img in images)
                {
                    form.Images.Add(img);
                }

                db.Forms.Add(form);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return Create();
        }
    }
}