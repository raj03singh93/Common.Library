using Common.Library.Enum;
using Common.Library.Handler;
using Common.Library.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Library.Extensions
{
    public static class CustomServiceExtentions
    {
        //public static IServiceCollection AddSwaggerService(this IServiceCollection services, string title, string description, string bearer, string version)
        //{
        //    services.AddSwaggerGen(options =>
        //    {
        //        //Document settings
        //        options.SwaggerDoc(version, new OpenApiInfo
        //        {
        //            Title = title,
        //            Version = version,
        //            Description = description,
        //        });

        //        //Security definition
        //        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        //        {
        //            Description = "JWT Authorization header {token}",
        //            Name = "Authorization",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer"
        //        });

        //        //Security binding - requirement
        //        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //        {
        //            {
        //                new OpenApiSecurityScheme()
        //                {
        //                    Reference = new OpenApiReference()
        //                    {
        //                        Type = ReferenceType.SecurityScheme,
        //                        Id = "Bearer"
        //                    },
        //                    Scheme = "oauth2",
        //                    Name = "Bearer",
        //                    In =  ParameterLocation.Header
        //                },
        //                new string[] { bearer }
        //            }
        //        });

        //        //Add operation filter to check the authorization token
        //        //options.OperationFilter<AuthorizeCheckOperationFilter>(bearer);
        //    });

        //    return services;
        //}

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

        public static IServiceCollection AddCustomSwaggerDocWithAuthentication(this IServiceCollection services, string name, ApiAuthenticationType type, OpenApiInfo apiInfo, bool addXmlDoc = false)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(name, apiInfo);

                GetAuthDefinitionValues(type, out string authName, out string scheme, out string description, out ParameterLocation loc, out SecuritySchemeType securitySchemeType);
                swagger.AddSecurityDefinition(scheme, new OpenApiSecurityScheme()
                {
                    Name = authName,
                    Description = description,
                    In = loc,
                    Scheme = scheme,
                    Type = securitySchemeType
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = scheme
                            }
                        },
                        new string[] { }
                    }
                });

                // Add docs for more details and set the comments path for the Swagger JSON and UI.    
                if (addXmlDoc)
                {
                    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    swagger.IncludeXmlComments(xmlPath);
                }
            });
            return services;
        }
        /// <summary>
        /// Add custom Jwt Authentication with jwt settings
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="jwtSetting">jwtSetting</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services, JwtTokenModel jwtSetting)
        {
            if (string.IsNullOrWhiteSpace(jwtSetting.Issuer)) throw new ArgumentNullException(nameof(jwtSetting.Issuer));
            if (string.IsNullOrWhiteSpace(jwtSetting.Key)) throw new ArgumentNullException(nameof(jwtSetting.Key));

            services.AddAuthentication(option => 
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(option => 
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    { 
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtSetting.Audiance ?? jwtSetting.Issuer,
                        ValidIssuer = jwtSetting.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key))
                    };
                });
            return services;
        }

        #region Private Fields
        private static void GetAuthDefinitionValues(ApiAuthenticationType type, out string authName, out string scheme, out string description, out ParameterLocation loc, out SecuritySchemeType securitySchemeType)
        {
            authName = string.Empty;
            scheme = string.Empty;
            description = string.Empty;
            loc = ParameterLocation.Header;
            securitySchemeType = SecuritySchemeType.ApiKey;
            switch (type)
            {
                case ApiAuthenticationType.Basic:
                    scheme = nameof(ApiAuthenticationType.Basic);
                    authName = $"{scheme} Authentication";
                    description = $"{scheme} Authentication with user and password.";
                    securitySchemeType = SecuritySchemeType.Http;
                    break;
                case ApiAuthenticationType.Bearer:
                    scheme = nameof(ApiAuthenticationType.Bearer);
                    authName = $"Jwt {scheme} Authentication";
                    description = $"{scheme} Authentication with jwt token.";
                    securitySchemeType = SecuritySchemeType.ApiKey;
                    break;
            }
        }
        #endregion
    }
}
