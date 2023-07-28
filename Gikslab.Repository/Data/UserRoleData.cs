using Gikslab.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gikslab.Repository.Data
{
    public static class UserRoleData
    {
        private static readonly User admin;

        static UserRoleData()
        {
            admin = new User
            {
                Id = new Guid("c3e40e7f-aca2-4f55-ae88-44a44f64dd12").ToString(),
                Name = "Administrator",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                PhoneNumber = "082121486155",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }

        #region snippet_Initialize

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RepositoryContext(serviceProvider.GetRequiredService<DbContextOptions<RepositoryContext>>()))
            {
                var roles = new[] { "board", "expert", "trainer", "competitor" };

                if (!context.Roles.Any())
                {
                    foreach (var role in roles)
                    {
                        var roleStore = new RoleStore<IdentityRole>(context);

                        await roleStore.CreateAsync(new IdentityRole
                        {
                            Name = role,
                            NormalizedName = role.ToUpper()
                        });
                    }
                }

                if (!context.Users.Any())
                {
                    await SeedUser(admin, context);
                }
            }
        }

        private static async Task SeedUser(User admin, RepositoryContext context)
        {
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(admin, "admin123");
            admin.PasswordHash = hashed;
            var userStore = new UserStore<User>(context);
            await userStore.CreateAsync(admin);
            await userStore.AddToRoleAsync(admin, "board");
            await context.SaveChangesAsync();
        }
        #endregion
    }
}
