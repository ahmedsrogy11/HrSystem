using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Dtos
{
    public record LeaveRequestDto(
        Guid Id,
        Guid EmployeeId,
        DateTime StartDate,
        DateTime EndDate,
        string Type,
        string Status,
        string? Reason,
        Guid? ApprovedByEmployeeId,
        DateTime? ApprovedAtUtc
    );
}
