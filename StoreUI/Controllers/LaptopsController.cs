using BLL.Implementation;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreUI.Controllers
{
    public class LaptopsController : Controller
    {
        // GET: Laptops
        private readonly ILaptopService _laptopService;

       
        public LaptopsController(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "LaptopStore";
            ViewBag.Laptops=_laptopService.GetAllLaptops();
            return View();
        }
    }
}