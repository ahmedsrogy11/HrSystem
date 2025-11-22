using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Abstractions
{
    public interface ITeamRepository
    {
        Task<Team?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<Team> Items, int Total)> ListAsync(
            Guid? departmentId,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(Team team, CancellationToken ct);
        Task UpdateAsync(Team team, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
