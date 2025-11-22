using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using HrSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Repositories
{
    public class OvertimeRequestRepository : IOvertimeRequestRepository
    {
        private readonly AppDbContext _db;

        public OvertimeRequestRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<OvertimeRequest?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.OvertimeRequests
                .Include(x => x.Employee)
                .Include(x => x.ApprovedByEmployee)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<(IReadOnlyList<OvertimeRequest> Items, int Total)> ListAsync(
            Guid? employeeId,
            OvertimeStatus? status,
            DateTime? dateFrom,
            DateTime? dateTo,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.OvertimeRequests.AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(x => x.EmployeeId == employeeId.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (dateFrom.HasValue)
                query = query.Where(x => x.Date >= dateFrom.Value);

            if (dateTo.HasValue)
                query = query.Where(x => x.Date <= dateTo.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(x => x.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(OvertimeRequest entity, CancellationToken ct)
        {
            await _db.OvertimeRequests.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(OvertimeRequest entity, CancellationToken ct)
        {
            _db.OvertimeRequests.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.OvertimeRequests.FindAsync(new object[] { id }, ct);
            if (entity is null) return;

            _db.Remove(entity); // ❗ Soft Delete by SaveChangesAsync override
            await _db.SaveChangesAsync(ct);
        }
    }
}
