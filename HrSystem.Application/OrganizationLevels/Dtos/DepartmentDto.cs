using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Dtos
{
    public record DepartmentDto(
       Guid Id,
       string Name,
       string? Code,
       Guid BranchId
    );


}
