namespace DAL
{
    using DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {
            Database.SetInitializer(new LaptopInitializer());
        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<LaptopType> LaptopTypes { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
    }
}