using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeForPets.Infrastructure;
using HomeForPets.Models;
using ModelDB;

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

            form.CreateDate = DateTime.Now;
            //Временная заглушка
            form.ProfileID = 1;

            images = ImageService.SaveImage(files);

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

            return RedirectToAction("Confirm", "Form", form.FormID);
        }

        [HttpGet]
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

            return View(form);
        }
    }
}