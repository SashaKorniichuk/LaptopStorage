﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreUI.Controllers
{
    public class LaptopsController : Controller
    {
        // GET: Laptops
        public ActionResult Index()
        {
            ViewBag.Title = "LaptopStore";
            return View();
        }
    }
}