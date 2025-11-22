using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Dtos
{
    public record AttendanceRecordDto(
        Guid Id,
        Guid EmployeeId,
        DateTime ClockInUtc,
        DateTime? ClockOutUtc,
        bool IsLate,
        bool IsAbsent,
        string? Source
    );
}
