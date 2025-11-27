using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Auth.Dtos
{
    public class RegisterUserResponseDto
    {
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
