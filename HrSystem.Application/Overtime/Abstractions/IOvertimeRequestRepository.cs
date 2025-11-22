using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Abstractions
{
    public interface IOvertimeRequestRepository
    {
        Task<OvertimeRequest?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<OvertimeRequest> Items, int Total)> ListAsync(
            Guid? employeeId,
            OvertimeStatus? status,
            DateTime? dateFrom,
            DateTime? dateTo,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(OvertimeRequest entity, CancellationToken ct);
        Task UpdateAsync(OvertimeRequest entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
