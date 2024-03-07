using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HouseholdIncomeAndExpensesWebbApp.Data
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
    }
}