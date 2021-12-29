using Common.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class CustomExtentions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, string title, string description, string bearer, string version)
        {
            services.AddSwaggerGen(options =>
            {
                //Document settings
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                    Description = description,
                });

                //Security definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //Security binding - requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In =  ParameterLocation.Header
                        },
                        new string[] { bearer }
                    }
                });

                //Add operation filter to check the authorization token
                //options.OperationFilter<AuthorizeCheckOperationFilter>(bearer);
            });

            return services;
        }

        /// <summary>
        /// Adds following HttpMessageHandlers -
        /// 1. JwtHttpMessageHandler - It adds Jwt bearer Token to header.
        /// </summary>
        /// <param name="services">services</param>
        /// <returns>services</returns>
        public static IServiceCollection AddCustomHttpMessageHandle(this IServiceCollection services)
        {
            services.ConfigureAll<HttpClientFactoryOptions>(options =>
            {
                options.HttpMessageHandlerBuilderActions.Add(builder =>
                {
                    builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<JwtHttpMessageHandler>());
                });
            });

            return services;
        }
    }
}
