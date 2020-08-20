using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDishesApp.API.Authorization;
using MyDishesApp.API.Database;
using MyDishesApp.API.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace MyDishesApp.API
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
                   .OfType<JsonInputFormatter>().FirstOrDefault();

                if (jsonInputFormatter != null)
                {
                    jsonInputFormatter.SupportedMediaTypes
                   .Add("application/vnd.robin.createdish+json");
                }
            })
            .AddJsonOptions(options =>
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

            // register the DbContext on the container, getting the connection string from
            // appsettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            string connectionString = Configuration["ConnectionStrings:MyDishesAppDB"];
            services.AddDbContext<DishInfoContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IDishInfoRepository, DishInfoRepository>();

            // register an IHttpContextAccessor so we can access the current
            // HttpContext in services by injecting it
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register the user info service
            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44398";
                    options.ApiName = "dishesmanagementapi";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DishInfoContext dishInfoContext)
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

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Database.Entities.Dish, Dtos.DishDto>();
                config.CreateMap<Database.Entities.Dish, Dtos.DishForUpdateDto>().ReverseMap();
                config.CreateMap<Dtos.IngredientForCreationDto, Database.Entities.Ingredient>();
                config.CreateMap<Database.Entities.Ingredient, Dtos.IngredientForUpdateDto>().ReverseMap();
            });

            // Enable CORS
            app.UseCors("AllowAllOriginsHeadersAndMethods");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}