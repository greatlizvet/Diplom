using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelDB
{
    public class ModelDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<OrderForRegistration> OrderForRegistrations { get; set; }
        public DbSet<City> Cities { get; set; }

        public ModelDbContext(string connectString) : base(connectString) { }
    }
}
