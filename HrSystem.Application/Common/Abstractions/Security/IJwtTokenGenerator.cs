using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Common.Abstractions.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
               Guid userId,
               Guid employeeId,
               string email,
               string fullName,
               IReadOnlyList<string> roles);
    }
}
