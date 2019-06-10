using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DomainOperations;
using API.DomainOperations.Interfaces;
using API.DomainServices;
using API.DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.IOC
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkTimeService, WorkTimeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IContractOperations, ContractOperations>();
            services.AddScoped<IHolidayOperations, HolidayOperations>();
            services.AddScoped<IUserOperations, UserOperations>();
            services.AddScoped<IWorkTimeOperations, WorkTimeOperations>();
            services.AddScoped<IDepartmentOperations, DepartmentOperations>();

            services.AddScoped<DbContext, HrApplicationContext>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                    .ActionContext;
                return new UrlHelper(actionContext);
            });
        }
    }
}
