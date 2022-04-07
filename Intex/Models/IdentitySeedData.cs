using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "mixer1kitchen@toyotaFord";

        private const string mfaUser = "mfaAdmin";
        private const string mfaPassword = "group34Intex$spring";

        public static async void EnsurePopulated (IApplicationBuilder app)
        {
            AppIdentityDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            IdentityUser userMfa = await userManager.FindByIdAsync(mfaUser);

            if (user == null)
            {
                user = new IdentityUser(adminUser);

                user.Email = "admin@utahcollisionresources.com";
                user.PhoneNumber = "555.123.4567";

                await userManager.CreateAsync(user, adminPassword);
            }

            if (userMfa == null)
            {
                userMfa = new IdentityUser(mfaUser);

                userMfa.Email = "mfa@utahcollisionresources.com";
                userMfa.PhoneNumber = "555.321.2121";
                userMfa.TwoFactorEnabled = true;

                await userManager.CreateAsync(userMfa, mfaPassword);
            }
        }
    }
}
