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

        static PetsDbContext()
        {
            Database.SetInitializer(new PetsInitializer());
        }

        public static PetsDbContext Create()
        {
            return new PetsDbContext();
        }
    }

    public class PetsInitializer : DropCreateDatabaseIfModelChanges<PetsDbContext>
    {
        protected override void Seed(PetsDbContext context)
        {
            context.Categories.Add(new Category { CategoryName = "Собаки" });
            context.Categories.Add(new Category { CategoryName = "Кошки" });

            context.Species.Add(new Specie { SpecieName = "Бульдог" });
            context.Species.Add(new Specie { SpecieName = "Овчарка" });
            context.Species.Add(new Specie { SpecieName = "Турецкая ангора" });
            context.Species.Add(new Specie { SpecieName = "Вислоухая шотландская" });
            context.Species.Add(new Specie { SpecieName = "Беспородная" });

            context.SaveChanges();
        }
    }
}