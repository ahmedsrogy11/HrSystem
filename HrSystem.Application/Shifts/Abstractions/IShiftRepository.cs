using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Abstractions
{
    public interface IShiftRepository
    {
        Task<Shift?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Shift>> ListAsync(CancellationToken ct);
        Task AddAsync(Shift shift, CancellationToken ct);
        Task UpdateAsync(Shift shift, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
