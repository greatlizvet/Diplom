using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ModelDB;

namespace HomeForPets.Infrastructure
{
    public class PetsDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Form> Forms { get; set; }
    }
}