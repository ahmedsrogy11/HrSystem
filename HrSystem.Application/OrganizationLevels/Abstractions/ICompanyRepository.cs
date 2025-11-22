using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Abstractions
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<Company> Items, int Total)> ListAsync(
            Guid? organizationId,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(Company company, CancellationToken ct);
        Task UpdateAsync(Company company, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
