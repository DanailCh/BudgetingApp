using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configurations
{
    public static class ConfigurationHelper
    {
        public static IdentityUser GuestUser = GetGuestUser();
        public static IdentityUser AdminUser = GetAdminUser();

        public static BillType ElectricityType = new BillType()
        {
            Id = 1,
            Name = "Electricity",
           
        };

        public static BillType WaterType = new BillType()
        {
            Id = 2,
            Name = "Water",
           
        };
        public static BillType HeatType = new BillType()
        {
            Id = 3,
            Name = "Heat",
           
        };
        public static BillType InternetType = new BillType()
        {
            Id = 4,
            Name = "Internet",
            
        };
        public static BillType RentType = new BillType()
        {
            Id = 5,
            Name = "Rent",
           
        };

        private static IdentityUser GetGuestUser()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                UserName = "guest@guest.bg",
                NormalizedUserName = "GUEST@GUEST.BG",
            };
            user.PasswordHash = hasher.HashPassword(user, "guest123");
            return user;
        }
        private static IdentityUser GetAdminUser()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                UserName = "admin@admin.bg",
                NormalizedUserName = "ADMIN@ADMIN.BG",
            };
            user.PasswordHash = hasher.HashPassword(user, "admin123");
            return user;
        }
    }
}
