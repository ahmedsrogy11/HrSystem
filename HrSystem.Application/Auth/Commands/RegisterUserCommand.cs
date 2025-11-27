using HrSystem.Application.Auth.Dtos;
using HrSystem.Application.Common.Abstractions.Identity;
using HrSystem.Application.Employees.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Auth.Commands
{
    public record RegisterUserCommand(
         Guid EmployeeId,
         string Email,
         string UserName,
         string Password
    ) : IRequest<RegisterUserResponseDto>;



    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, RegisterUserResponseDto>
    {
        private readonly IEmployeeRepository _employees;
        private readonly IIdentityService _identity;

        public RegisterUserCommandHandler(
            IEmployeeRepository employees,
            IIdentityService identity)
        {
            _employees = employees;
            _identity = identity;
        }

        public async Task<RegisterUserResponseDto> Handle(RegisterUserCommand r, CancellationToken ct)
        {
            var employee = await _employees.GetByIdAsync(r.EmployeeId, ct);

            if (employee is null)
            {
                throw new Exception("Employee not found");
            }

            // 2️⃣ نستخدم IdentityService لإنشاء User مربوط بالموظف
            var (success, error, userId) =
                await _identity.RegisterAsync(r.EmployeeId, r.Email, r.UserName, r.Password, ct);

            if (!success)
            {
                throw new Exception($"User registration failed: {error}");
            }


            return new RegisterUserResponseDto
            {
                UserId = userId,
                EmployeeId = r.EmployeeId,
                Email = r.Email,
                UserName = r.UserName
            };
        }

    }
}
