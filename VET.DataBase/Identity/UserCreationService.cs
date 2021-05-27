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
            await this.userManager.CreateAsync(user, "Test@123*");
            await this.roleManager.CreateAsync(role);
            await this.userManager.AddToRoleAsync(user, "Admin");

            var role1 = new IdentityRole { Name = "UserEmployee", NormalizedName = "USEREMPLOYEE", ConcurrencyStamp = "UserEmployee", };
            var user1 = new ApplicationUser { UserName = "Employee", Email = "Employee@vet.com", };
            await this.userManager.CreateAsync(user1, "Test@123*");
            await this.roleManager.CreateAsync(role1);
            await this.userManager.AddToRoleAsync(user1, "UserEmployee");
        }
    }
}