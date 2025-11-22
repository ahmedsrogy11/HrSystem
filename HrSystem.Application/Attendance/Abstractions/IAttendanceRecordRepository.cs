using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Abstractions
{
    public interface IAttendanceRecordRepository
    {
        Task<AttendanceRecord?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<AttendanceRecord> Items, int Total)> ListAsync(
            Guid? employeeId,
            DateTime? dateFrom,
            DateTime? dateTo,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(AttendanceRecord record, CancellationToken ct);
        Task UpdateAsync(AttendanceRecord record, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);

        Task<AttendanceRecord?> GetTodayRecordForEmployeeAsync(
             Guid employeeId,
             CancellationToken ct);
    }
}
