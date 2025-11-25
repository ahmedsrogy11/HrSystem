using HrSystem.Application.Payroll.Abstractions;
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
    public class PayrollPeriodRepository : IPayrollPeriodRepository
    {
        private readonly AppDbContext _db;

        public PayrollPeriodRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PayrollPeriod?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.PayrollPeriods
                .Include(p => p.Payslips)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<(IReadOnlyList<PayrollPeriod> Items, int Total)> ListAsync(
           int? year,
           int? month,
           DateTime? fromDate,
           DateTime? toDate,
           bool? isClosed,
           int page,
           int pageSize,
           CancellationToken ct)
        {
            var q = _db.PayrollPeriods.AsQueryable();

            if (year.HasValue)
                q = q.Where(x => x.Year == year);

            if (month.HasValue)
                q = q.Where(x => x.Month == month);

            if (fromDate.HasValue)
                q = q.Where(x => x.FromDate >= fromDate);

            if (toDate.HasValue)
                q = q.Where(x => x.ToDate <= toDate);

            if (isClosed.HasValue)
                q = q.Where(x => x.IsClosed == isClosed);

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(PayrollPeriod entity, CancellationToken ct)
        {
            await _db.PayrollPeriods.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(PayrollPeriod entity, CancellationToken ct)
        {
            _db.PayrollPeriods.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.PayrollPeriods.FindAsync(new object[] { id }, ct);
            if (entity is null) return;

            _db.Remove(entity); // هيطبق SoftDelete من SaveChangesAsync
            await _db.SaveChangesAsync(ct);
        }
    }
}
