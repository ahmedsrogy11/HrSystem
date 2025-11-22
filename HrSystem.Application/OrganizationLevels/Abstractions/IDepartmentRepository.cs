using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Abstractions
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<Department> Items, int Total)> ListAsync(
            Guid? branchId,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(Department department, CancellationToken ct);
        Task UpdateAsync(Department department, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
