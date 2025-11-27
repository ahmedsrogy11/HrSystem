using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Auth.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }

        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;

        public List<string> Roles { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }
}
