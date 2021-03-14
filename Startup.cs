using CarDealerAPI.Contexts;
using CarDealerAPI.Middlewere;
using CarDealerAPI.Profiles;
using CarDealerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Excepticon.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using CarDealerAPI.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models.Validators;
using CarDealerAPI.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarDealerAPI.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace CarDealerAPI
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
            var authSettings = new AuthenticationSettings();

            Configuration.GetSection("AuthenticationRysiek").Bind(authSettings);
            services.AddSingleton(authSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
                option.DefaultScheme = "Bearer";
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = false;
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authSettings.JwtIssuer,
                    ValidAudience = authSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)),
                };
            });

            services.AddAuthorization(policy =>
            {
                policy.AddPolicy("HasNation", b => b.RequireClaim("Nationality", "Poland"));
                policy.AddPolicy("ColorEyes", b => b.RequireClaim("ColorEye", "blue", "green", "grey"));
                policy.AddPolicy("OnlyForEagles", b => b.AddRequirements(new CheckAge(18)));

            });
            
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<DealerDbContext>();
            services.AddScoped<DealerSeeder>();
            services.AddScoped<IAuthorizationHandler, CheckAgeHandler>();
            services.AddScoped<IAuthorizationHandler, ResouceOperationRequirementHandler>();
            services.AddAutoMapper(typeof(DealerProfile).GetTypeInfo().Assembly);
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<RequestTimeMiddle>();
            services.AddScoped<IValidator<UserCreateDTO>, RegisterDtoValidator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarDEaler", Version = "v1" });
            });


            //services.AddAutoMapper(typeof(DealerProfile).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DealerSeeder seeder)
        {

            seeder.Seed();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    // Do work that doesn't write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from 2nd delegate.");
            //});
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddle>();
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarDealerAPI");
            });
            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
