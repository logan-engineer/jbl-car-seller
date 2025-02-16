using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Identity;


namespace jbl_car_seller.services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();
            try
            {
                logger.LogInformation("Database is created");
                await context.Database.EnsureCreatedAsync();
                //add role
                logger.LogInformation("Seeding roles");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "User");

                logger.LogInformation("Seeding roles");
                var adminemail = "admin@gmail.com";
                if (await userManager.FindByEmailAsync(adminemail) == null)
                {
                    var adminuser = new User
                    {
                        FullName = "logan tech",
                        Email = adminemail,
                        UserName = adminemail
                    };
                    var result = await userManager.CreateAsync(adminuser, "Admin@123");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assing Adminrole to admin user");
                        await userManager.AddToRoleAsync(adminuser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create Admin user:{Error}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "an error occured while seeding the data base");
            }


        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    throw new Exception($"failed to create role '{roleName}':{string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
