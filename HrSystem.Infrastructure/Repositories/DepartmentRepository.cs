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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _db;

        public DepartmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _db.Departments
                        .Include(d => d.Branch)
                        .FirstOrDefaultAsync(d => d.Id == id, ct);

        public async Task<(IReadOnlyList<Department> Items, int Total)> ListAsync(
            Guid? branchId,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.Departments.AsQueryable();

            if (branchId.HasValue)
                query = query.Where(d => d.BranchId == branchId.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(d => d.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(Department department, CancellationToken ct)
        {
            await _db.Departments.AddAsync(department, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Department department, CancellationToken ct)
        {
            _db.Departments.Update(department);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Departments.FirstOrDefaultAsync(d => d.Id == id, ct);
            if (entity is null) return;

            
            _db.Departments.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
