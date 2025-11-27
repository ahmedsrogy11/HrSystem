using HrSystem.Application.Common.Abstractions.Identity;
using HrSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public IdentityService(UserManager<AppUser> userManager , RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        // Assign role to user
        public async Task<(bool Success, string Error)> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return (false, "User not found.");
            }


            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return (false, "Role does not exist.");
            }


            var inRole = await _userManager.IsInRoleAsync(user, roleName);
            if (inRole)
            {
                return (false, "User is already in the role.");
            }


            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return (true, "");
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }
        }





        // Create role
        public async Task<(bool Success, string Error)> CreateRoleAsync(string roleName, CancellationToken ct)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (roleExists)
            {
                return (false, "Role already exists.");
            }

            var role = new IdentityRole<Guid>(roleName);

            var result = await _roleManager.CreateAsync(role);

            if (result != null) 
            {
                return (true, "");
            }
            else
            {
                return (false, "Failed to create role.");
            }

        }






        // Login user
        public async Task<(bool Success, string Error, Guid UserId, Guid EmployeeId, string FullName, string Email, IList<string> Roles)> 
            LoginAsync(string email, string password, CancellationToken ct)
        {
            // نضمن مفيش مسافات زيادة
            

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return (false, $"LOGIN_DEBUG: user not found for email '{email}'.",
                    Guid.Empty, Guid.Empty, "", "", new List<string>());
            }

            var valid = await _userManager.CheckPasswordAsync(user, password);
            if (!valid)
            {
                return (false, "LOGIN_DEBUG: password not valid.",
                    Guid.Empty, Guid.Empty, "", "", new List<string>());
            }

            var roles = await _userManager.GetRolesAsync(user);

            return (true, "", user.Id, user.EmployeeId, user.UserName ?? "", user.Email!, roles);
        }








        // Register a new user
        public async Task<(bool Success, string Error, Guid UserId)> 
            RegisterAsync(Guid EmployeeId, string email, string userName, string password, CancellationToken ct)
        {
            var existingUser =await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return (false, "Email is already registered.", Guid.Empty);


            var newUser = new AppUser
            {
                EmployeeId = EmployeeId,
                UserName = userName,
                Email = email
            };


            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return (false, errors, Guid.Empty);
            }

            return (true, "", newUser.Id);
        }
    }
}
