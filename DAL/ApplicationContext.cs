using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
namespace DAL
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {
            Database.SetInitializer(new LaptopInitializer());
        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<LaptopType> LaptopTypes { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }




    }
}