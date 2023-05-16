﻿using Business.Configuration;
using Business.Models;
using Business.Repositories;
using Business.Repositories.Interfaces;
using Business.Services;
using Business_Logic_Layer.Configuration;
using Data;
using Data.Domain;
using Data.Domain.Entities;
using Data.Services;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddControllers();//x => x.Filters.Add<ApiKeyAuthFilter>()
            services.AddOptions();
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(configs =>
            {
                configs.AddPolicy(
                    "AllowAllOrigins",
                    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
            services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<MappingProfile>();
            });

            // DA
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEntityService<Department>, DepartmentService>();
            services.AddScoped<IEntityService<Employee>, EmployeeService>();
            // BL
            services.AddScoped<IAccountLogic, AccountLogic>();
            services.AddScoped<ILogic<DepartmentModel>, DepartmentLogic>();
            services.AddScoped<ILogic<EmployeeModel>, EmployeeLogic>();

            
            services.AddMemoryCache(setup =>
            {
                //setup.SizeLimit = 1000;
                //setup.ExpirationScanFrequency.Add()
            });

            // Database

            services.AddDbContextPool<CompanyContext>(options =>
            {
                Console.WriteLine("initial company");
                //var connection = options.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Headers["Individual-Code"];
                options.UseSqlServer(_configuration.GetConnectionString("Company"), b => b.MigrationsAssembly("API"));
            });
            services.AddDbContextPool<Company1Context>(options => 
            {
                Console.WriteLine("Initial company1");
                options.UseSqlServer(_configuration.GetConnectionString("Company1"), b => b.MigrationsAssembly("API"));
            });
            
            services.AddScoped<MultiContext>();

            // Identity
            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CompanyContext>()
                .AddDefaultTokenProviders();
            //.AddAuthenticatorApp();

            services.Configure<IdentityOptions>(opt =>
            {
                // Password
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;

                // User
                opt.User.RequireUniqueEmail = true;

                // Sign in
                opt.SignIn.RequireConfirmedEmail = true;


                // Lock
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            });

            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = bool.Parse(
                            _configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]
                        ),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                _configuration["JsonWebTokenKeys:IssuerSigninKey"]
                            )
                        ),
                        ValidateIssuer = bool.Parse(
                            _configuration["JsonWebTokenKeys:ValidateIssuer"]
                        ),
                        ValidIssuer = _configuration["JsonWebTokenKeys:ValidIssuer"],
                        ValidateAudience = bool.Parse(
                            _configuration["JsonWebTokenKeys:ValidateAudience"]
                        ),
                        ValidAudience = _configuration["JsonWebTokenKeys:ValidAudience"],
                        ValidateLifetime = bool.Parse(
                            _configuration["JsonWebTokenKeys:ValidateLifetime"]
                        ),
                        RequireExpirationTime = bool.Parse(
                            _configuration["JsonWebTokenKeys:RequireExpirationTime"]
                        ),
                    };
                });

            //token
            // services.Configure<DataProtectionTokenProviderOptions>();
            /*
            services.Configure<EmployeeController>(config =>
            {
                config.options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetPriority(CacheItemPriority.Normal)
                    .SetSize(100);
            });
            */
            //Mail
            var emailConfig = _configuration
                .GetSection("EmailConfiguration")
                .Get<MailConfiguration>();

            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailService>();

            // Dapper

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseCors(configs => configs.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors("AllowAllOrigins");
            //app.UseMiddleware<ApiKeyAuthMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}