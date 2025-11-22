using HrSystem.Application.Shifts.Abstractions;
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
    public class ShiftAssignmentRepository : IShiftAssignmentRepository
    {
        private readonly AppDbContext _db;

        public ShiftAssignmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(ShiftAssignment entity, CancellationToken ct)
        {
            await _db.ShiftAssignments.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var item = await _db.ShiftAssignments.FindAsync(new object[] { id }, ct);
            if (item is null) return;

            _db.Remove(item); // SoftDelete
            await _db.SaveChangesAsync(ct);
        }

        public async Task<ShiftAssignment?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.ShiftAssignments
                .Include(x => x.Shift)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<(IReadOnlyList<ShiftAssignment> Items, int Total)> ListAsync(
            Guid? employeeId,
            Guid? shiftId,
            DateTime? dateFrom,
            DateTime? dateTo,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var q = _db.ShiftAssignments.AsQueryable();

            if (employeeId.HasValue)
                q = q.Where(x => x.EmployeeId == employeeId);

            if (shiftId.HasValue)
                q = q.Where(x => x.ShiftId == shiftId);

            if (dateFrom.HasValue)
                q = q.Where(x => x.FromDate >= dateFrom);

            if (dateTo.HasValue)
                q = q.Where(x => x.ToDate <= dateTo);

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(x => x.FromDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task UpdateAsync(ShiftAssignment entity, CancellationToken ct)
        {
            _db.ShiftAssignments.Update(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
