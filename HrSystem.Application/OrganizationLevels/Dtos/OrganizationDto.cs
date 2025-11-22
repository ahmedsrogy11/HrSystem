using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Organizations.Dtos
{
    public record OrganizationDto(
       Guid Id,
       string Name,
       string? Code
    );
}
