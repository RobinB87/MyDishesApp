using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDishesApp.WebApi.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using Microsoft.Extensions.Hosting;
using MyDishesApp.Repository.Data;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Services;
using MyDishesApp.WebApi.Dtos;

namespace MyDishesApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserMustBeAdministrator", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Administrator");
                });
                options.AddPolicy(
                    "UserMustBeDishManager",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.AddRequirements(new UserMustBeDishManagerRequirement("Administrator"));
                    });
            });

            services.AddScoped<IAuthorizationHandler, UserMustBeDishManagerRequirementHandler>();

            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

                // output formatters..

                // input formatters
                var jsonInputFormatter = setupAction.InputFormatters
                   .OfType<SystemTextJsonInputFormatter>().FirstOrDefault();

                jsonInputFormatter?.SupportedMediaTypes.Add("application/vnd.robin.createdish+json");
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });

            // Configure CORS so the API allows requests from JavaScript.  
            // For demo purposes, all origins/headers/methods are allowed.  
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // Register the DbContext on the container, getting the connection string from appsettings
            var connectionString = Configuration["ConnectionStrings:MyDishesAppDB"];
            services.AddDbContext<DishesContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IDishRepository, DishRepository>();

            // register an IHttpContextAccessor so we can access the current
            // HttpContext in services by injecting it
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register the user info service
            services.AddScoped<IUserInfoService, UserInfoService>();

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "https://localhost:44398";
            //        options.ApiName = "dishesmanagementapi";
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            // TODO: Create mapping profile class
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Dish, DishDto>();
                config.CreateMap<Dish, DishForUpdateDto>().ReverseMap();
                config.CreateMap<IngredientForCreationDto, Ingredient>();
                config.CreateMap<Ingredient, IngredientForUpdateDto>().ReverseMap();
            });

            // Enable CORS
            app.UseCors("AllowAllOriginsHeadersAndMethods");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}