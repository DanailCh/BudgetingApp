﻿using App.Core.Contracts;
using App.Core.Services;
using App.Infrastructure.Data.Configurations;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IHouseholdService, HouseholdService>();
            services.AddScoped<IBillTypeService, BillTypeService>();
            services.AddScoped<IBudgetSummaryService, BudgetSummaryService>();
            services.AddScoped<ISummaryLogicService, SummaryLogicService>();
            services.AddScoped<IFileGeneratorService, FileGeneratorService>();
            services.AddScoped<IFeedBackMessageService, FeedbackMessageService>();
           
            services.AddScoped<IAdminService, AdminService>();


            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services,IConfiguration config)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
