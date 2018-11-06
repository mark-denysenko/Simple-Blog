using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Interfaces;
using EFProvider;
using Interfaces;
using Services;
using Services.Interfaces;

namespace WebUI.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<HasherPassword>().As<IHasherPassword>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<BlogService>().As<IBlogService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}