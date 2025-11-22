using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Abstractions
{
    public interface ILeaveRequestRepository
    {
        Task<LeaveRequest?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<LeaveRequest> Items, int Total)> ListAsync(
            Guid? employeeId,
            LeaveStatus? status,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(LeaveRequest leave, CancellationToken ct);
        Task UpdateAsync(LeaveRequest leave, CancellationToken ct);
    }
}
