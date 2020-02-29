using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelDB;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace HomeForPets.Infrastructure
{
    public class ProjectDbContext : ModelDbContext
    {
        public ProjectDbContext() : base("ProjectDbContext") { }

        static ProjectDbContext()
        {
            Database.SetInitializer(new PetsInitializer());
        }

        public static ProjectDbContext Create()
        {
            return new ProjectDbContext();
        }
    }

    public class PetsInitializer : DropCreateDatabaseIfModelChanges<ProjectDbContext>
    {
        protected override void Seed(ProjectDbContext context)
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

        public void PerformInitialSetup(ProjectDbContext context)
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