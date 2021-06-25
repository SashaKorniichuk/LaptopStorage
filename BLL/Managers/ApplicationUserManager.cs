using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;



namespace BLL.Managers
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store):base(store)
        {
                
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> option,IOwinContext owinContext)
        {
            var dbContext = owinContext.Get<DbContext>();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail=true

            };

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                //RequireDigit=true,
               // RequireLowercase=true,
               // RequireNonLetterOrDigit=true,
               // RequireUppercase=true
            };

            return manager;
        }
    }
}