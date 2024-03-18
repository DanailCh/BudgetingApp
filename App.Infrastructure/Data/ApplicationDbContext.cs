using App.Infrastructure.Data.Configurations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Payer)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.BillType)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.BillTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberSalary>()
                .HasOne(m => m.HouseholdMember)
                .WithMany(h => h.MemberSalaries)
                .HasForeignKey(m => m.HouseholdMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BillTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new TaskConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}