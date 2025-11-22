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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _db;

        public CompanyRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Company?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _db.Companies
                        .Include(c => c.Organization)
                        .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<(IReadOnlyList<Company> Items, int Total)> ListAsync(
            Guid? organizationId,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.Companies.AsQueryable();

            if (organizationId.HasValue)
                query = query.Where(c => c.OrganizationId == organizationId.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(Company company, CancellationToken ct)
        {
            await _db.Companies.AddAsync(company, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Company company, CancellationToken ct)
        {
            _db.Companies.Update(company);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (entity is null) return;

            _db.Companies.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
