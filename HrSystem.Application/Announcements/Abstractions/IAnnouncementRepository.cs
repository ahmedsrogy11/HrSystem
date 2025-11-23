using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.Abstractions
{
    public interface IAnnouncementRepository
    {
        Task<Announcement?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<Announcement> Items, int Total)> ListAsync(
            DateTime? dateFrom,
            DateTime? dateTo,
            bool? isGlobal,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(Announcement entity, CancellationToken ct);
        Task UpdateAsync(Announcement entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
