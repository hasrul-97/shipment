using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shipment.Query.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Query
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
            services.AddControllers();

            //  AZURE ACTIVE DIRECTORY AUTHENTICATION

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddJwtBearer(options =>
            // {
            //     options.Authority = String.Format(Configuration["AzureAd:AadInstance"], Configuration["AzureAD:Tenant"]);
            //     options.Audience = Configuration["AzureAd:Audience"];

            //     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidIssuer = Configuration["AzureAD:issuer"],
            //         ValidateAudience = true,
            //         ValidAudience = Configuration["AzureAD:ClientId"],
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AzureAD:key"])),
            //         ValidateLifetime = true
            //     };
            //     options.SaveToken = true;
            //     options.Events = new JwtBearerEvents
            //     {

            //         OnTokenValidated = context =>
            //         {
            //             context.Principal = new ClaimsPrincipal(
            //             new ClaimsIdentity(context.Principal.Claims, "local"));


            //             return Task.CompletedTask;
            //         },
            //         OnAuthenticationFailed = authFailedcontext =>
            //         {
            //             return Task.CompletedTask;
            //         }
            //     };
            // });


            //  JWT TOKEN VALIDATION (AUTHORIZATION)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["AzureAd:Issuer"],
                    ValidAudience = Configuration["AzureAd:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AzureAd:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        context.Principal = new ClaimsPrincipal(new ClaimsIdentity(context.Principal.Claims, "local"));
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //  SWAGGER DOCUMENTATION
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "Version 1",
                    Title = "Shipment API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
            });

            //  DEPENDENCY RESOLVER
            DependencyResolver.ConfigureService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shipment API, Version 1");
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
