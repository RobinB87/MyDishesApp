using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyDishesApp.Service.MappingProfiles;
using System;
using System.IO;
using System.Reflection;

namespace MyDishesApp.WebApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAssembliesToAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }

        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "Dishes App Api",
                Description = "WebApi that exposes dishes",
                Version = configuration.GetValue<string>("Version")
            };

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("webapi", openApiInfo);
                c.IncludeXmlComments(xmlPath);

                // Authorization is not enabled but for calls to FSO the token needs to be passed
                // on. By enabling the security requirements on swagger the user can pass the token
                // directly from swagger ui. Note that this won't affect the way .net core handles
                // authorization just the header itself.
               // c.AddSecurityDefinition("Bearer",
               //new OpenApiSecurityScheme
               //{
               //    In = ParameterLocation.Header,
               //    Description = "Please enter into field the word 'Bearer' following by space and JWT",
               //    Name = "Authorization",
               //    Type = SecuritySchemeType.ApiKey,
               //    BearerFormat = "Bearer {token}"
               //});
               // c.AddSecurityRequirement(new OpenApiSecurityRequirement()
               // {
               //     {
               //         new OpenApiSecurityScheme
               //         {
               //             Reference = new OpenApiReference
               //             {
               //                 Type = ReferenceType.SecurityScheme,
               //                 Id = "Bearer"
               //             },
               //             Scheme = "oauth2",
               //             Name = "Bearer",
               //             In = ParameterLocation.Header,
               //         },
               //         new List<string>()
               //     }
               // });
            });
        }
    }
}