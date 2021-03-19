using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
   public class LaptopInitializer : DropCreateDatabaseAlways<ApplicationContext>
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
                new Laptop()
                {
                    Name="Gaming Laptop x15",
                 
                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://assets3.razerzone.com/dwXtXeE3zt-fVh1g_MgnDYwj6HM=/767x511/https%3A%2F%2Fhybrismediaprod.blob.core.windows.net%2Fsys-master-phoenix-images-container%2Fhf6%2Fh14%2F9088499712030%2FRZ09-0330QEM3-R3U1-gallery01.jpg",
                Developer = new Developer(){ Name="Lenovo"},
                LaptopType =new LaptopType(){Name="Gaming"}
                },
           
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
