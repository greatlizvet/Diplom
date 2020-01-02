using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HomeForPets.Models;
using ModelDB;

namespace HomeForPets.Infrastructure
{ 
    public static class FormViewService
    {
        public static FormCreateViewModel InitializeFormCreate()
        {
            FormCreateViewModel formCreate = new FormCreateViewModel();
            formCreate = (FormCreateViewModel)InitilizeForm(formCreate);

            return formCreate;
        }

        public static FormListViewModel InitializeFormList()
        {
            FormListViewModel formList = new FormListViewModel();
            formList = (FormListViewModel)InitilizeForm(formList);

            return formList;
        }

        private static IFormView InitilizeForm(IFormView model)
        {
            PetsDbContext db = new PetsDbContext();
            List<Category> categories = db.Categories.ToList();
            List<Specie> species = db.Species.ToList();

            model.Categories = new SelectList(categories, "CategoryID", "CategoryName");
            model.Species = new SelectList(species, "SpecieID", "SpecieName");

            return model;
        }
    }
}