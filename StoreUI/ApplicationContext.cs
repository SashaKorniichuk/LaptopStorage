namespace StoreUI
{
    using DLL.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationContext : DbContext
    {
     
        public ApplicationContext()
            : base("name=ApplicationContext")
        {
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Laptop> Laptops { get; set; }

        public DbSet<Laptop> Laptops { get; set; }

    }
}