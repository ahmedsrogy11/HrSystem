using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Dtos
{
    public record ShiftAssignmentDto(
        Guid Id,
        Guid EmployeeId,
        Guid ShiftId,
        DateTime FromDate,
        DateTime ToDate
    );
}
