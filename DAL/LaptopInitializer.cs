using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
   public class LaptopInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            //add items...
            var devs = new List<Developer>
            {
                new Developer(){Name="Lenovo"},
            };

            var laptops = new List<Laptop>
            {
                new Laptop(){Name="Gaming Laptop x15"},
           
            };

            var types = new List<LaptopType>
            {
                new LaptopType(){Name="Gaming"},
            };

            context.Developers.AddRange(devs);
            context.LaptopTypes.AddRange(types);
            context.Laptops.AddRange(laptops);
            context.SaveChanges();

            base.Seed(context);
        }

    }
}
