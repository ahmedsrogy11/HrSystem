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
    public class ShiftRepository : IShiftRepository
    {
        private readonly AppDbContext _db;

        public ShiftRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Shift shift, CancellationToken ct)
        {
            await _db.Shifts.AddAsync(shift, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Shifts.FindAsync(new object[] { id }, ct);
            if (entity is null) return;

            _db.Remove(entity); // SoftDelete by SaveChangesAsync override
            await _db.SaveChangesAsync(ct);
        }

        public async Task<Shift?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Shifts.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyList<Shift>> ListAsync(CancellationToken ct)
        {
            return await _db.Shifts.OrderBy(x => x.StartTime).ToListAsync(ct);
        }

        public async Task UpdateAsync(Shift shift, CancellationToken ct)
        {
            _db.Shifts.Update(shift);
            await _db.SaveChangesAsync(ct);
        }
    }
}
