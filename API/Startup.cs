using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using API.Data;
using API.DTO;
using API.DTO.Contract;
using API.DTO.Department;
using API.DTO.Holiday;
using API.DTO.User;
using API.DTO.WorkTime;
using API.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => 
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<HrApplicationContext>(options =>
                options
                    //.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString));
            
            IOC.Dependencies.Register(services);

            // Swagger generator registration, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Human Resources interface",
                    Version = "v1",
                    Description = "This API provides an access point for any consumers (clients of any kind)" +
                                  "to make calls against any of the provided calls. Any calls which require a " +
                                  "specific role are as such secured and documented. The main goal of this API " +
                                  "is not to provide a bold new re-imagining of the possibilities of APIs, nor " +
                                  "does it completely adhere to all REST principles (such as HATEOAS). Instead, " +
                                  "it means to offer an example of a well documented REST-like application interface. " +
                                  "\n\nThis API is hosted in Microsoft Azure. This deployment process is fully automated. " +
                                  "Every check in on the Github repo automatically triggers an Azure build and deploy. " +
                                  "This ensures that the version running in production is always the latest possible version." +
                                  "Additionally, an Azure Logic App was also used to trigger the sending out of " +
                                  "user creation emails."
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            InitializeMaps();

            app.UseHttpsRedirection();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Human Resources API V1");
            });


        }

        private static void InitializeMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserReturnDto>();
                cfg.CreateMap<User, UserReturnMinifiedDto>();
                cfg.CreateMap<User, ManagerReturnDto>();
                cfg.CreateMap<NewUserDto, User>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                        $"{src.FirstName} {src.LastName}"))
                    .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src =>
                        src.Birthdate));
                cfg.CreateMap<User, UserPasswordChangedReturnDto>();
                cfg.CreateMap<int, UserDeletedReturnDto>()
                    .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src =>
                        $"User with userId {src} was removed."));
                cfg.CreateMap<User, UserChangedReturnDto>();
                cfg.CreateMap<User, UserContractUpdatedReturnDto>();
                cfg.CreateMap<User, UserContractRemovedReturnDto>();

                cfg.CreateMap<Department, DepartmentReturnDto>();
                cfg.CreateMap<Department, DepartmentReturnMinifiedDto>();

                cfg.CreateMap<Contract, ContractReturnDto>();
                cfg.CreateMap<ContractDto, Contract>();

                cfg.CreateMap<WorkTime, WorkTimeReturnDto>()
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.WorkDateTime));
                cfg.CreateMap<WorkTime,WorkTimeReturnMinifiedDto>()
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.WorkDateTime));

                cfg.CreateMap<Holiday, HolidayReturnDto>()
                    .ForMember(dest => dest.HolidayId, opt => opt.MapFrom(src => src.ID));
                cfg.CreateMap<Holiday, HolidayReturnMinifiedDto>()
                    .ForMember(dest => dest.HolidayId, opt => opt.MapFrom(src => src.ID));
                cfg.CreateMap<Holiday, HolidayCreatedReturnDto>();

            });
        }
    }
}
