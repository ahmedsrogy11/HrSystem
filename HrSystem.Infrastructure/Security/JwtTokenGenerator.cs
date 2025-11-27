using HrSystem.Application.Common.Abstractions.Security;
using HrSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(
            Guid userId,
            Guid employeeId,
            string email,
            string fullName,
            IReadOnlyList<string> roles)
        {
              var key = _configuration["Jwt:Key"];

              var issuer = _configuration["Jwt:Issuer"];

              var audience = _configuration["Jwt:Audience"];

              var expiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60");


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),

                new Claim("userId" , userId.ToString()),

                new Claim("employeeId", employeeId.ToString()),

                new Claim(ClaimTypes.Email, email),

                new Claim("fullName", fullName)

            };


            foreach (var r in roles.Distinct())
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
      