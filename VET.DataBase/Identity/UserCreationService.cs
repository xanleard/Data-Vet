// <copyright file="UserCreationService.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public class UserCreationService : IUserCreationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserCreationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task CreateUser()
        {
            var role = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "Admin", };
            var user = new ApplicationUser { UserName = "Administrador", Email = "admin@admin.com", };
            await this.userManager.CreateAsync(user, "Casino@123");
            await this.roleManager.CreateAsync(role);
            await this.userManager.AddToRoleAsync(user, "Admin");
            var user1 = new ApplicationUser { UserName = "Test", Email = "test@test.com", };
            await this.userManager.CreateAsync(user1, "Casino@123");
            await this.userManager.AddToRoleAsync(user1, "Admin");
            var role1 = new IdentityRole { Name = "SeeTicks", NormalizedName = "SEETICKS", ConcurrencyStamp = "SeeTicks", };
            await this.roleManager.CreateAsync(role1);
        }
    }
}