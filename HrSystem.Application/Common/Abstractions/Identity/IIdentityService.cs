using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Common.Abstractions.Identity
{
    public interface IIdentityService
    {
        Task<(bool Success, string Error, Guid UserId, Guid EmployeeId, string FullName, string Email, IList<string> Roles)>
            LoginAsync(string email, string password, CancellationToken ct);


        Task <(bool Success, string Error, Guid UserId)> 
            RegisterAsync(Guid EmployeeId, string userName, string email, string password, CancellationToken ct);


        Task<(bool Success, string Error)> CreateRoleAsync(string roleName, CancellationToken ct);


        Task<(bool Success, string Error)> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken ct);

    }
}
