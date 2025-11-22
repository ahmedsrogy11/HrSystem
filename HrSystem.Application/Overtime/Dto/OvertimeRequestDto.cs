using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Dto
{
    public record OvertimeRequestDto(
        Guid Id,
        Guid EmployeeId,
        DateTime Date,
        decimal Hours,
        string Status,
        Guid? ApprovedByEmployeeId,
        DateTime? ApprovedAtUtc
    );
}
