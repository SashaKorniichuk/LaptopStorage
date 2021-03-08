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

            var dev1 = new Developer()
            {
                Name = "Lenovo"
            };

            var LaptopType = new LaptopType()
            {
                Name = "Gaming"
            };

            var Laptop = new Laptop()
            {
                Name = "Gaming Laptop x15"
            };

            context.Developers.Add(dev1);
            context.LaptopTypes.Add(LaptopType);
            context.Laptops.Add(Laptop);
            context.SaveChanges();

            base.Seed(context);
        }

    }
}
