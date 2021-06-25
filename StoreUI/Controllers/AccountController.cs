using BLL.DTO;
using BLL.Interfaces;
using BLL.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StoreUI.Models;
using StoreUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoreUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILaptopService _laptopService;

        private ApplicationUserManager manager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationSigninManager signinManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationSigninManager>();
            }
        }


        public AccountController(IAccountService accountService, ILaptopService laptopService)
        {
            _accountService = accountService;
            _laptopService = laptopService;
        }
        public ActionResult Login()
        {
            return View();
        }
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            

            var userDTO = new ApplicationUserDTO
            {

                UserName = model.UserName,
                Email = model.Email,

            };
            var user = _accountService.GetNewApplicationUser(userDTO);

            var identityResult = await manager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                await manager.AddToRoleAsync(manager.FindByName(user.UserName).Id, "User");
                await signinManager.SignInAsync(user, false, false);

                if (user.Carts == null)
                {

                    CartDTO cart = new CartDTO()
                    {
                        UserId = user.Id,
                        MakeOrder = false,
                        State = "In Process"
                    };

                    await _laptopService.AddCart(cart);
                }
                await manager.UpdateAsync(user);

                return RedirectToAction("Index", "Laptops");
            }
            else
            {
                ModelState.AddModelError("", string.Join(", ", identityResult.Errors));
            }
            return View(model);
            
        }

        public async Task<ActionResult> SignIn(LoginUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await signinManager.PasswordSignInAsync(model.Login, model.Password, false, false);

                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Laptops");
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong name or password.");
            }
            return View("Login",model);
        }
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}