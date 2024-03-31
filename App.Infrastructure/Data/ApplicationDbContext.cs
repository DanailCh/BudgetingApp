﻿using App.Infrastructure.Data.Configurations;
using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace HouseholdBudgetingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Bill> Bills { get; init; }
        public DbSet<BillType> BillTypes { get; init; }
        public DbSet<HouseholdBudget> HouseholdBudgets { get; init; }
        public DbSet<HouseholdMember> HouseholdMembers { get; init; }
        public DbSet<MemberSalary> MemberSalaries { get; init; }
        public DbSet<EndMonthSummary> EndMonthSummaries { get; init; }
        public DbSet<FeedbackMessage> FeedbackMessages { get; init; }
        public DbSet<SeverityType> SeverityTypes { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new BillTypeConfiguration());
            
            modelBuilder.ApplyConfiguration(new SeverityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackMessageConfiguration());
            modelBuilder.ApplyConfiguration(new MemberSalaryConfiguration());           
            

            base.OnModelCreating(modelBuilder);
        }

    }
}