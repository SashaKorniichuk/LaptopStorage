using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web;
using DAL.Repository;

namespace DAL
{
    public class LaptopInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(store);


            var devs = new List<Developer>
            {
                new Developer(){Name="Lenovo"},
                new Developer(){Name="Apple"},
                new Developer(){Name="Asus"},
                new Developer(){Name="Xiaomi"},
                new Developer(){Name="Samsung"},
            };
            var types = new List<LaptopType>
            {
                new LaptopType(){Name="Gaming"},
                new LaptopType(){Name="Office"},
                new LaptopType(){Name="Design"},
            };
            var laptops = new List<Laptop>
            {
                new Laptop()
                {
                    Name="Lenovo IdeaPad",

                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://support.content.office.net/uk-ua/media/382cd7ac-4dbe-482f-85fa-ab506ccbec34.png",
                Developer = devs.FirstOrDefault(x=>x.Name=="Lenovo"),
                LaptopType =types.FirstOrDefault(x=>x.Name=="Office")
                },
                new Laptop()
                {
                    Name="MacBook Pro 13",

                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://surface-pro.ru/wp-content/uploads/2019/12/Microsoft-Surface-Laptop-3-15-review-32.jpg",
                Developer = devs.FirstOrDefault(x=>x.Name=="Apple"),
                LaptopType =types.FirstOrDefault(x=>x.Name=="Design")
                },
                new Laptop()
                {
                    Name="Gaming Laptop x15",

                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://banggood.ir/11924-thickbox_default/%D9%84%D9%BE-%D8%AA%D8%A7%D9%BE-xiaomi-mi-gaming-laptop-core-i7-rtx-2060.jpg",
                Developer = devs.FirstOrDefault(x=>x.Name=="Xiaomi"),
                LaptopType =types.FirstOrDefault(x=>x.Name=="Gaming")
                },
                new Laptop()
                {
                    Name="Gaming Laptop x15",

                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTs0L2PvDoRI9EcyugEHTdLtRUftZPlhaqdremwfi43kwoxs2rlZlcXCvIUN_DSqXkJB3o&usqp=CAU",
                Developer = devs.FirstOrDefault(x=>x.Name=="Asus"),
                LaptopType =types.FirstOrDefault(x=>x.Name=="Gaming")
                },
                new Laptop()
                {
                    Name="Lenovo IdeaPad Gaming3",

                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/macbook-air-space-gray-select-201810?wid=892&hei=820&&qlt=80&.v=1603332211000",
                Developer = devs.FirstOrDefault(x=>x.Name=="Lenovo"),
                LaptopType =types.FirstOrDefault(x=>x.Name=="Gaming")
                },
                new Laptop()
                {
                    Name="Asus ROG Strix",

                Processor = "1",
                RAM ="1",
                VideoCard = "1",
                Disc = "1",
                Price = 100,
                Description = "1",
                Image = "https://images-na.ssl-images-amazon.com/images/I/61s7sJEpsVL._SY450_.jpg",
                Developer = devs.FirstOrDefault(x=>x.Name=="Asus"),
                LaptopType =types.FirstOrDefault(x=>x.Name=="Design")
                },

            };

            var roleAdmin = new IdentityRole()
            {
                Name = "Admin"
            };

            var roleUser = new IdentityRole()
            {
                Name = "User"
            };

            context.Roles.Add(roleAdmin);
            context.Roles.Add(roleUser);


            userManager.Create(new ApplicationUser
            {
                Email = "admin@gmail.com",
                UserName = "admin"
            }, "123456");

            userManager.Create(new ApplicationUser
            {
                Email = "user@gmail.com",
                UserName = "user",
             
            }, "123456");
            context.SaveChanges();

            userManager.AddToRole(userManager.FindByName("admin").Id, "Admin");
            userManager.AddToRole(userManager.FindByName("user").Id, "User");



            context.Developers.AddRange(devs);
            context.LaptopTypes.AddRange(types);
            context.Laptops.AddRange(laptops);
            
            context.SaveChanges();

            base.Seed(context);
        }

    }
}
