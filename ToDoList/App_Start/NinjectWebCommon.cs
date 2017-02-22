[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ToDoList.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ToDoList.App_Start.NinjectWebCommon), "Stop")]

namespace ToDoList.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Infra.CrossCutting.Identity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using Microsoft.Owin.Security.DataHandler.Serializer;
    using Microsoft.Owin.Security.DataProtection;
    using Controllers;
    using Infra.CrossCutting.IoC;
    using Application.Interfaces;
    using Application.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            BootStrapper.RegisterServices(kernel);

            #region identity            
            kernel.Bind<IAuthenticationManager>().ToMethod(context => 
            {
                var contextBase = new HttpContextWrapper(HttpContext.Current);
                return contextBase.GetOwinContext().Authentication;
            });
            #endregion            

            #region token
            kernel.Bind<ISecureDataFormat<AuthenticationTicket>>()
                .To<SecureDataFormat<AuthenticationTicket>>();
            kernel.Bind<ITextEncoder>()
                .To<Base64UrlTextEncoder>();
            kernel.Bind<IDataSerializer<AuthenticationTicket>>()
                .To<TicketSerializer>();
            kernel.Bind<IDataProtector>()
                .ToMethod(context => new DpapiDataProtectionProvider().Create("Asp.Net Identity"));
            #endregion

            

            #region controller
            kernel.Bind<AccountController>()
                .ToSelf()
                .WithConstructorArgument("userManager", kernel.Get<UserManager<ApplicationUser>>())
                .WithConstructorArgument("accessTokenFormat", kernel.Get <ISecureDataFormat<AuthenticationTicket>>());                  
            #endregion
        }
    }
}
