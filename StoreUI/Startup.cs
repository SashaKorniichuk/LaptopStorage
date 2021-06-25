using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BLL;
using BLL.Managers;
using DAL;
using DAL.Entities;
using DAL.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using StoreUI.Utils;

[assembly: OwinStartup(typeof(StoreUI.Startup))]

namespace StoreUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DbContext>(() => new ApplicationContext());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSigninManager>(ApplicationSigninManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType=DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath=new PathString("/Account/Login")
            });
        }
       
    }
}
