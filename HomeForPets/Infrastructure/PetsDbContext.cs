using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ModelDB;
using HomeForPets.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeForPets.Infrastructure
{
    public class PetsDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<OrderForRegistration> OrderForRegistrations { get; set; }

        public PetsDbContext() : base("PetsDbContext") { }

        static PetsDbContext()
        {
            Database.SetInitializer(new PetsInitializer());
        }

        public static PetsDbContext Create()
        {
            return new PetsDbContext();
        }
    }

    public class PetsInitializer : DropCreateDatabaseAlways<PetsDbContext>
    {
        protected override void Seed(PetsDbContext context)
        {
            var categories = new List<Category>
            {
                new Category { CategoryName = "Собаки" },
                new Category { CategoryName = "Кошки" }
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var species = new List<Specie>
            {
                new Specie { SpecieName = "Бульдог", CategoryID = categories.Single(c => c.CategoryName == "Собаки").CategoryID},
                new Specie { SpecieName = "Овчарка", CategoryID = categories.Single(c => c.CategoryName == "Собаки").CategoryID},
                new Specie { SpecieName = "Турецкая ангора", CategoryID = categories.Single(c => c.CategoryName == "Кошки").CategoryID},
                new Specie { SpecieName = "Вислоухая шотландская", CategoryID = categories.Single(c => c.CategoryName == "Кошки").CategoryID},
                new Specie { SpecieName = "Беспородная", CategoryID = categories.Single(c => c.CategoryName == "Кошки").CategoryID},
                new Specie { SpecieName = "Беспородная", CategoryID = categories.Single(c => c.CategoryName == "Собаки").CategoryID}
            };

            species.ForEach(s => context.Species.Add(s));
            context.SaveChanges();
        }
    }
}