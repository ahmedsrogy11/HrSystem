using HrSystem.Application.Auth.Dtos;
using HrSystem.Application.Common.Abstractions.Identity;
using HrSystem.Application.Common.Abstractions.Security;
using HrSystem.Application.Employees.Abstractions;
using HrSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HrSystem.Application.Auth.Commands
{
    public record LoginCommand(string Email, string Password)
      : IRequest<LoginResponseDto>;



    public class LoginCommandHandler
       : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IIdentityService _identity;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IConfiguration _config;

        public LoginCommandHandler(
            IIdentityService identity,
            IJwtTokenGenerator jwt,
            IConfiguration config)
        {
            _identity = identity;
            _jwt = jwt;
            _config = config;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand r, CancellationToken ct)
        {
            // 👈 مهم جدًا: نشيل أي مسافات قبل/بعد الإيميل
            var trimmedEmail = r.Email.Trim(); 

            // 👇 كل الشغل الخاص بـ Identity جوّه Infrastructure
            var (success, error, userId, employeeId, fullName, identityEmail, roles) =
                await _identity.LoginAsync(trimmedEmail, r.Password, ct);

            if (!success)
                throw new Exception(error);

            var token = _jwt.GenerateToken(
                userId: userId,
                employeeId: employeeId,
                email: identityEmail,
                fullName: fullName,
                roles: roles.ToList());

            var expiryMinutes = int.Parse(_config["Jwt:ExpiryMinutes"] ?? "60");

            return new LoginResponseDto
            {
                Token = token,
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(expiryMinutes),
                UserId = userId,
                EmployeeId = employeeId,
                Email = identityEmail,
                FullName = fullName,
                Roles = roles.ToList()
            };

        }
        
            
    }
}
