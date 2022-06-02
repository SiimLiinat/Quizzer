using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.App.EF.Seeding
{
    public static class AppDataInit
    {
        public static void DropDatabase(AppDbContext context, ILogger logger)
        {
            logger.LogInformation("DropDatabase");
            context.Database.EnsureDeleted();
        }
        
        public static void MigrateDatabase(AppDbContext context, ILogger logger)
        {
            logger.LogInformation("MigrateDatabase");
            context.Database.Migrate();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            ILogger logger)
        {
            logger.LogInformation("SeedIdentity");
            foreach (var roleName in InitialData.Roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = roleName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        Console.WriteLine("Errors:");
                        Console.Write(result.Errors);
                        throw new ApplicationException("Role creation failed");
                    }
                    logger.LogInformation("Seeded role {Role}", roleName);
                }
            }

            foreach (var userInfo in InitialData.Users)
            {
                var user = userManager.FindByNameAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = userInfo.name,
                        Email = userInfo.email
                    };

                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed");
                    }
                    logger.LogInformation("Seeded user {User}", userInfo.name);
                }
                
                var roleResult = userManager.AddToRolesAsync(user, userInfo.roles).Result;
            }
        }
        
        public static void SeedData(AppDbContext context, ILogger logger)
        {
            
        }
    }
}