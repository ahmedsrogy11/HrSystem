using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Abstractions
{
    public interface IBranchRepository
    {
        Task<Branch?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<Branch> Items, int Total)> ListAsync(
            Guid? companyId,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(Branch branch, CancellationToken ct);
        Task UpdateAsync(Branch branch, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
