using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using BLL.Implementation;
using BLL.Interfaces;
using DAL;
using DAL.Repository;
using DAL.Repository.Interface;

namespace StoreUI.Utils
{
    public static class AutofacConfiguration
    {
        public static void Configurate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationContext>().As<DbContext>();
            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<LaptopService>().As<ILaptopService>();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

        }
    }
}