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
        public static FormCreateViewModel InitializeFormCreate(int categoryId = 1, int specieId = 1)
        {
            FormCreateViewModel formCreate = new FormCreateViewModel();
            formCreate = (FormCreateViewModel)InitilizeForm(formCreate, categoryId, specieId);

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
            ProjectDbContext db = new ProjectDbContext();
            List<Category> categories = db.Categories.ToList();
            List<Specie> species = db.Species.Where(s => s.CategoryID == 1).ToList();
            List<City> cities = db.Cities.ToList();

            var type = model.GetType();
            if(type.Name == "FormListViewModel")
            {
                categories.Insert(0, new Category { CategoryName = "Все", CategoryID = 0 });
                species.Insert(0, new Specie { SpecieName = "Все", SpecieID = 0, CategoryID = 0 });

                model.Categories = new SelectList(categories, "CategoryID", "CategoryName", 0);
            }
            else
            {
                model.Categories = new SelectList(categories, "CategoryID", "CategoryName", 1);
            }

            model.Species = new SelectList(species, "SpecieID", "SpecieName");
            model.Cities = new SelectList(cities, "CityID", "Name");

            return model;
        }

        private static IFormView InitilizeForm(IFormView model, int categoryId, int specieId)
        {
            ProjectDbContext db = new ProjectDbContext();
            List<Category> categories = db.Categories.ToList();
            List<Specie> species = db.Species.Where(s => s.CategoryID == categoryId).ToList();

            model.Categories = new SelectList(categories, "CategoryID", "CategoryName", categoryId);
            model.Species = new SelectList(species, "SpecieID", "SpecieName", specieId);

            return model;
        }
    }
}