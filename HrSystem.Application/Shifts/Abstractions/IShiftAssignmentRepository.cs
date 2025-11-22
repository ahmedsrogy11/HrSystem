using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Abstractions
{
    public interface IShiftAssignmentRepository
    {
        Task<ShiftAssignment?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<ShiftAssignment> Items, int Total)> ListAsync(
            Guid? employeeId,
            Guid? shiftId,
            DateTime? dateFrom,
            DateTime? dateTo,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(ShiftAssignment entity, CancellationToken ct);
        Task UpdateAsync(ShiftAssignment entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
