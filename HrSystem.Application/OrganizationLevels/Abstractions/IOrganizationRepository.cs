using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Abstractions
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<Organization> Items, int Total)> ListAsync(
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(Organization organization, CancellationToken ct);
        Task UpdateAsync(Organization organization, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
