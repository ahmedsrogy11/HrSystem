using HrSystem.Application.Announcements.Abstractions;
using HrSystem.Domain.Entities;
using HrSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _db;

        public AnnouncementRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Announcement entity, CancellationToken ct)
        {
            await _db.Announcements.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Announcements.FindAsync(new object[] { id }, ct);
            if (entity is null) return;

            _db.Remove(entity); // Soft delete
            await _db.SaveChangesAsync(ct);
        }

        public async Task<Announcement?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Announcements
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<(IReadOnlyList<Announcement> Items, int Total)> ListAsync(
            DateTime? dateFrom,
            DateTime? dateTo,
            bool? isGlobal,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.Announcements.AsQueryable();

            if (dateFrom.HasValue)
                query = query.Where(x => x.PublishAtUtc >= dateFrom.Value);

            if (dateTo.HasValue)
                query = query.Where(x => x.PublishAtUtc <= dateTo.Value);

            if (isGlobal.HasValue)
                query = query.Where(x => x.IsGlobal == isGlobal.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(x => x.PublishAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task UpdateAsync(Announcement entity, CancellationToken ct)
        {
            _db.Announcements.Update(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
