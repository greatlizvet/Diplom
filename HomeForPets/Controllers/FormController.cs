using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using HomeForPets.Models;
using ModelDB;
using Microsoft.AspNet.Identity;

namespace HomeForPets.Controllers
{
    public class FormController : Controller
    {
        PetsDbContext db = new PetsDbContext();

        FormCreateViewModel formCreate = FormViewService.InitializeFormCreate();
        

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
            return View(formCreate);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Form form, HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();

            if(ModelState.IsValid)
            {
                images = ImageService.SaveImage(files);
                form.AppUserId = User.Identity.GetUserId();

                foreach (var img in images)
                {
                    db.Images.Add(img);
                }

                foreach (var img in images)
                {
                    form.Images.Add(img);
                }

                db.Forms.Add(form);
                db.SaveChanges();

                return Redirect("/Form/Confirm/" + form.FormID);
            }

            return View(formCreate);
        }
        
        public ActionResult Confirm(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            Form form = db.Forms.Find(id);

            if(form == null)
            {
                return HttpNotFound();
            }

            formCreate = FormViewService.InitializeFormCreate(form.CategoryID, form.SpecieID);
            formCreate.Form = form;

            return View(formCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(Form form, HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();

            if(ModelState.IsValid)
            {
                Form newForm = db.Forms.Find(form.FormID);
                images = newForm.Images.ToList();

                newForm.FormName = form.FormName;
                newForm.Age = form.Age;
                newForm.CategoryID = form.CategoryID;
                newForm.Color = form.Color;
                newForm.Desieses = form.Desieses;
                newForm.Difficulties = form.Difficulties;
                newForm.Sex = form.Sex;
                newForm.SpecieID = form.SpecieID;
                newForm.Specificity = form.Specificity;
                newForm.Temperament = form.Temperament;
                newForm.UnPublished = false;
                newForm.CreateDate = DateTime.Now;

                if(files.First() != null)
                {
                    images = SetImages(files);
                    newForm.Images = images;
                }

                db.Entry(newForm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            formCreate = FormViewService.InitializeFormCreate(form.CategoryID, form.SpecieID);
            formCreate.Form = form;

            return View(formCreate);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            Form form = db.Forms.Find(id);

            if(form == null)
            {
                return HttpNotFound();
            }

            formCreate.Form = form;

            return View(formCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Form form, HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();

            if(ModelState.IsValid)
            {
                if(files.First() != null)
                {
                    images = SetImages(files);
                }

                db.Entry(form).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Profile");
            }

            formCreate.Form = form;

            return View(formCreate);
        }

        public ActionResult Disable(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Form form = db.Forms.Find(id);

            if (form == null)
            {
                return HttpNotFound();
            }

            form.Enable = false;

            db.Entry(form).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Account");
        }

        public JsonResult GetSpecies(int id)
        {
            List<Specie> species = db.Species.Where(s => s.CategoryID == id).ToList();
            formCreate.Species = new SelectList(species, "SpecieID", "SpecieName");

            return Json(formCreate, JsonRequestBehavior.AllowGet);
        }

        private List<Image> SetImages(HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();
            images = ImageService.SaveImage(files);

            foreach (var image in images)
            {
                db.Images.Add(image);
            }

            return images;
        }
    }
}