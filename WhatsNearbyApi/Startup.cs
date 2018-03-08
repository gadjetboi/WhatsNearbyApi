using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsNearbyApi.Entities;
using WhatsNearbyApi.Interfaces;
using WhatsNearbyApi.Models;
using WhatsNearbyApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;

namespace WhatsNearbyApi
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            var connectionString = _configuration["Database:ConnectionString"];

            //Reference to setup sqllite https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite
            services.AddDbContext<WhatsNearbyApiContext>(o => o.UseSqlite(connectionString));
          
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddIdentity<CustomUser, IdentityRole>()
                .AddEntityFrameworkStores<WhatsNearbyApiContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["IssuerSigningKey"])),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.RequireHttpsMetadata = Convert.ToBoolean(_configuration["RequireHttpsMetadata"]);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperUsers", policy => policy.RequireClaim("SuperUser", "True"));
                //TODO: Optional, add policy against the role
            });
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
                        IApplicationBuilder app, 
                        IHostingEnvironment env, 
                        ILoggerFactory loggerFactory, 
                        WhatsNearbyApiContext WhatsNearbyApiContext)
        {
            app.UseCors("CorsPolicy");

            loggerFactory.AddNLog();    
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TODO: implement database seed

            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<CustomUser, CustomUserDto>();
            });

            app.UseAuthentication();

            app.UseMvc();
           
          }
    }
}
