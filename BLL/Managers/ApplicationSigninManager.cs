using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Managers
{
    public class ApplicationSigninManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSigninManager(UserManager<ApplicationUser, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSigninManager Create(IdentityFactoryOptions<ApplicationSigninManager> options,
            IOwinContext owinContext)
        {
            var userManager = owinContext.GetUserManager<ApplicationUserManager>();
            var signinManager = new ApplicationSigninManager(userManager, owinContext.Authentication);

            return signinManager;
        }
    }
}