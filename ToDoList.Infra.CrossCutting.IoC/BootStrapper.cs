using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Services;
using ToDoList.Infra.CrossCutting.Identity.Configuration;
using ToDoList.Infra.CrossCutting.Identity.Context;
using ToDoList.Infra.CrossCutting.Identity.Models;

namespace ToDoList.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void RegisterServices(IKernel kernel)
        {
            #region identity
            kernel.Bind<ApplicationDbContext>().ToSelf();
            kernel.Bind<IUserStore<ApplicationUser>>()
                .To<UserStore<ApplicationUser>>()
                .WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
            kernel.Bind<UserManager<ApplicationUser>>()
                .To<ApplicationUserManager>()
                .WithConstructorArgument("store", kernel.Get<IUserStore<ApplicationUser>>());
            #endregion


            #region application.service
            kernel.Bind<IAssignmentService>()
            .To<AssignmentService>();
            #endregion

        }
    }
}
