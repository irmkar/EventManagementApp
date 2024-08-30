using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace EventManagementApp.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Ensure the database is created
                context.Database.Migrate();

                // Check if any users exist
                if (!context.Users.Any())
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    // Seed roles
                    var adminRole = "Admin";
                    if (!await roleManager.RoleExistsAsync(adminRole))
                    {
                        await roleManager.CreateAsync(new IdentityRole(adminRole));
                    }

                    // Seed default admin user
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        EmailConfirmed = true
                    };

                    var userResult = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (userResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                }
            }
        }
    }
}
