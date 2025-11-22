using HrSystem.Application.OrganizationLevels.Abstractions;
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
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _db;

        public BranchRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _db.Branches
                        .Include(b => b.Company)
                        .FirstOrDefaultAsync(b => b.Id == id, ct);

        public async Task<(IReadOnlyList<Branch> Items, int Total)> ListAsync(
            Guid? companyId,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.Branches.AsQueryable();

            if (companyId.HasValue)
                query = query.Where(b => b.CompanyId == companyId.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(b => b.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(Branch branch, CancellationToken ct)
        {
            await _db.Branches.AddAsync(branch, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Branch branch, CancellationToken ct)
        {
            _db.Branches.Update(branch);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Branches.FirstOrDefaultAsync(b => b.Id == id, ct);
            if (entity is null) return;

            
            _db.Branches.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
