using AutoMapper;
using BLL.DTO;
using BLL.Filters;
using BLL.Interfaces;
using StoreUI.Models;
using StoreUI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StoreUI.Utils;
using BLL.Managers;

using Microsoft.AspNet.Identity.EntityFramework;

namespace StoreUI.Controllers
{
    public class LaptopsController : Controller
    {

        private readonly ILaptopService _laptopService;
        private readonly IMapper _mapper;
        private string userId { get { return User.Identity.GetUserId(); } }
        private ApplicationUserManager manager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }


        public LaptopsController(ILaptopService laptopService, IMapper mapper)
        {
            _laptopService = laptopService;
            _mapper = mapper;

        }
        public async Task<ActionResult> Index(string search)
        {
            setViewBag();
            ViewBag.Title = "LaptopStore";


            var currentUser = await manager.FindByIdAsync(userId);
            if (currentUser != null)
            {
                if (currentUser.Carts.Count == 0)
                {
                    await CreateCart();
                }
                if (currentUser.Carts.Count != 0)
                    ViewBag.BinCount = currentUser.Carts.LastOrDefault().cartItems.Count();
                ViewBag.OrdersCount = _laptopService.GetOrderCart().Where(x=>x.State=="In Process").ToList().Count;

            }
            var laptops = _laptopService.GetAllLaptops(null);
            if (String.IsNullOrEmpty(search))
            {
                ViewBag.Laptops = laptops;
                return View();
            }
            ViewBag.Laptops = laptops.Where(x => x.Name.Contains(search)).ToList();
            return View();

        }
        private void setViewBag()
        {
            ViewBag.Types = _laptopService.GetTypes();
            ViewBag.Developers = _laptopService.GetDevelopers();
            ViewBag.BinCount = 0;
        }
        public ActionResult Create()
        {
            SelectList types = new SelectList(_laptopService.GetAllTypes(), "Id", "Name");
            ViewBag.Types = types;
            SelectList devs = new SelectList(_laptopService.GetAllDevelopers(), "Id", "Name");
            ViewBag.Developers = devs;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(LaptopViewModel model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            if (image != null)
            {

                var fileName = Guid.NewGuid() + ".jpg";
                var bitmap = BitmapConvertor.Convert(image.InputStream, 200, 200);
                var serverPath = Server.MapPath($"~/Images/{fileName}");
                bitmap.Save(serverPath);
                model.Image = $"/Images/{fileName}";
            }

            await _laptopService.AddLaptopAsync(_mapper.Map<LaptopDTO>(model));
            return RedirectToAction("Index");
        }

        public ActionResult Filter(string type, string value)
        {
            var filter = new LaptopsFilter()
            {
                Name = value,
                Type = type
            };

            if (type == "developer")
            {
                filter.Predicate = (x => x.Developer.Name == value);
            }
            else if (type == "type")
            {
                filter.Predicate = (x => x.LaptopType.Name == value);
            }
            else if (type == "ram")
            {
                filter.Predicate = (x => x.RAM == value);

            }
            var filters = new List<LaptopsFilter>();
            if (Session["LaptopsFilter"] != null)
            {
                filters = Session["LaptopsFilter"] as List<LaptopsFilter>;
            }
            var found = filters.FirstOrDefault(f => f.Name == value && f.Type == type);

            if (found != null)
            {
                filters.Remove(found);
            }
            else
            {
                filters.Add(filter);
            }
            Session["LaptopsFilter"] = filters;


            var laptops = _laptopService.GetAllLaptops(filters);
            ViewBag.Laptops = laptops;

            return PartialView("LaptopsPartial", ViewBag);

        }
        private LaptopViewModel GetViewModel(int id)
        {
            LaptopDTO laptop = _laptopService.GetAllLaptops(null).Where(x => x.Id == id).FirstOrDefault();
            LaptopViewModel l = new LaptopViewModel()
            {
                Name = laptop.Name,
                Processor = laptop.Processor,
                RAM = laptop.RAM,
                VideoCard = laptop.VideoCard,
                Disc = laptop.Disc,
                Price = laptop.Price,
                Description = laptop.Description,
                Image = laptop.Image,
                Developer = _laptopService.GetAllDevelopers().Where(x => x.Id == laptop.DeveloperId).FirstOrDefault().Name,
                LaptopType = _laptopService.GetAllTypes().Where(x => x.Id == laptop.LaptopTypeId).FirstOrDefault().Name
            };
            return l;

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            return View(GetViewModel((int)id));
        }
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            await _laptopService.DeleteAsync((int)id);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            SelectList types = new SelectList(_laptopService.GetAllTypes(), "Id", "Name", id);
            ViewBag.Types = types;
            SelectList devs = new SelectList(_laptopService.GetAllDevelopers(), "Id", "Name", id);
            ViewBag.Developers = devs;

            return View(GetViewModel((int)id));
        }

        [HttpPost]

        public async Task<ActionResult> Edit(LaptopViewModel model, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var fileName = Guid.NewGuid() + ".jpg";
                var bitmap = BitmapConvertor.Convert(image.InputStream, 200, 200);
                var serverPath = Server.MapPath($"~/Images/{fileName}");
                bitmap.Save(serverPath);
                model.Image = $"/Images/{fileName}";
            }
            await _laptopService.EditAsync(_mapper.Map<LaptopDTO>(model));
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(GetViewModel(id));

        }
        public async Task<ActionResult> AddToCart(int id)
        {
            var user = await manager.FindByIdAsync(userId);
            CartItemDTO order = new CartItemDTO()
            {
                LaptopId = id,
                CartId = (user.Carts.LastOrDefault()).Id
            };

            await _laptopService.AddOrder(order);
            return RedirectToAction("Index");

        }

        private async Task CreateCart()
        {
            var user = await manager.FindByIdAsync(userId);

            if (user.Carts.Count == 0)
            {
                CartDTO cart = new CartDTO()
                {

                    UserId = userId,
                    MakeOrder = false,
                    State = "In Process"
                };
                await _laptopService.AddCart(cart);
            }

        }
        public async Task<ActionResult> Cart()
        {
            await SetCartViewBag();
            return View();
        }

        private async Task SetCartViewBag()
        {
            var currentUser = await manager.FindByIdAsync(userId);
            ViewBag.Items = currentUser.Carts.LastOrDefault().cartItems;
            ViewBag.BinCount = currentUser.Carts.LastOrDefault().cartItems.Count();
            ViewBag.Price = Price();
        }
        public async Task<ActionResult> DeleteOrder(int id)
        {

            await _laptopService.DeleteOrder(id);
            await SetCartViewBag();

            return RedirectToAction("Cart");
        }

        private double Price()
        {
            var currentUser = manager.FindById(userId);

            double? price = 0;

            for (int i = 0; i < currentUser.Carts.LastOrDefault().cartItems.Count(); i++)
            {
                price += (currentUser.Carts.LastOrDefault().cartItems).ToArray()[i].Laptop.Price;
            }
            return (double)price;
        }
        public async Task<ActionResult> MakeOrder()
        {
            var currentUser = await manager.FindByIdAsync(userId);

            var Cart = currentUser.Carts.LastOrDefault();
            if (Cart.cartItems.Count != 0)
            {

                var CartId = Cart.Id;
                CartDTO cart = new CartDTO()
                {
                    Id = CartId,
                    UserId = currentUser.Id,
                    State = "In Process",
                    MakeOrder = true
                };

                await _laptopService.EditCart(cart);

                CartDTO cart2 = new CartDTO()
                {
                    UserId = currentUser.Id,
                    MakeOrder = false,
                    State = "In Process"
                };
                await _laptopService.AddCart(cart2);
            }
            else
            {
                ModelState.AddModelError("", "Cart is empty");
                return RedirectToAction("Cart");

            }
            ViewBag.Items = currentUser.Carts.LastOrDefault().cartItems;
            //ViewBag.Admins = admins.FirstOrDefault().Email;
            return RedirectToAction("Index");
        }

        public ActionResult Orders()
        {

            return View(_laptopService.GetOrderCart());

        }
        public ActionResult OrderDetails(int id)
        {
            var cardItems = _laptopService.GetCartItems(id);

            return View(_mapper.Map<IEnumerable<OrderDetailsViewModel>>(cardItems));
        }

        public ActionResult History()
        {
            var orders = _laptopService.GetOrderCart().Where(x => x.MakeOrder == true && x.UserId == userId);
            ViewBag.OrdersCount = _laptopService.GetOrderCart().Where(x => x.State == "In Process").ToList().Count;
            return View(_mapper.Map<IEnumerable<HistoryViewModel>>(orders));
        }

        public async Task<ActionResult> AcceptOrder(int id)
        {
            var Cart = _laptopService.GetCart(id);
            Cart.State = "Accepted";
            await _laptopService.UpdateStateCart(Cart);
            return RedirectToAction("Orders");

        }

        public async Task<ActionResult> DenyOrder(int id)
        {
            var Cart = _laptopService.GetCart(id);
            Cart.State = "Deny";
            await _laptopService.UpdateStateCart(Cart);
            return RedirectToAction("Orders");

        }
    }
}