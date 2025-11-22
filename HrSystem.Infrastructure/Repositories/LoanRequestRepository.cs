using HrSystem.Application.Loans.Abstractions;
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
    public class LoanRequestRepository : ILoanRequestRepository
    {
        private readonly AppDbContext _db;

        public LoanRequestRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<LoanRequest?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.LoanRequests
                .Include(l => l.Employee)
                .Include(l => l.ApprovedByEmployee)
                .FirstOrDefaultAsync(l => l.Id == id, ct);
        }

        public async Task<(IReadOnlyList<LoanRequest> Items, int Total)> ListAsync(
            Guid? employeeId,
            LoanStatus? status,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.LoanRequests.AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(x => x.EmployeeId == employeeId.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(x => x.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(LoanRequest loan, CancellationToken ct)
        {
            await _db.LoanRequests.AddAsync(loan, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(LoanRequest loan, CancellationToken ct)
        {
            _db.LoanRequests.Update(loan);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.LoanRequests.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (entity is null) return;

            
            _db.LoanRequests.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
