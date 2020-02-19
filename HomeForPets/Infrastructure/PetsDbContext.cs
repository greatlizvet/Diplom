using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelDB;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace HomeForPets.Infrastructure
{
    public class PetsDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<OrderForRegistration> OrderForRegistrations { get; set; }
        public DbSet<City> Cities { get; set; }

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

    public class PetsInitializer : DropCreateDatabaseIfModelChanges<PetsDbContext>
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

            var cities = new List<City>
            {
                new City {Name = "Кемерово"},
                new City {Name = "Москва"},
                new City {Name = "Санкт-Петербург"},
                new City {Name = "Новосибирск"},
                new City {Name = "Красноярск"}
            };

            cities.ForEach(c => context.Cities.Add(c));
            context.SaveChanges();

            PerformInitialSetup(context);
            context.SaveChanges();

            base.Seed(context);
        }

        public void PerformInitialSetup(PetsDbContext context)
        {
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrator";
            string userName = "Admin";
            string password = "Rtvthjdj0227!";
            string email = "elizavetaaleshkevitch@yandex.ru";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }

            AppUser user = userManager.FindByName(userName);
            if(user == null)
            {
                userManager.Create(new AppUser { UserName = userName, Email = email, CityID = 1 }, password);
                user = userManager.FindByName(userName);
            }

            if(!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}