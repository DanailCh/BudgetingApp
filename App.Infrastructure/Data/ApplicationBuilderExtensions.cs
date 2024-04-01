
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App.Infrastructure.Data
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager= services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if(await roleManager.RoleExistsAsync("Administrator"))
                {
                    return; 
                }
                else 
                {
                    var role = new IdentityRole { Name = "Administrator" };
                    await roleManager.CreateAsync(role);
                    var admin = await userManager.FindByNameAsync("admin");
                    await userManager.AddToRoleAsync(admin, role.Name);
                }


                if (await roleManager.RoleExistsAsync("Guest"))
                {
                    return;
                }
                else
                {
                    var roleGuest = new IdentityRole { Name = "Guest" };
                    await roleManager.CreateAsync(roleGuest);
                    var guest = await userManager.FindByNameAsync("guest");
                    await userManager.AddToRoleAsync(guest, roleGuest.Name);
                }
                
            })
                .GetAwaiter().GetResult();
            return app;
        }
    }
}
