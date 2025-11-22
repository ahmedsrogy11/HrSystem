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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _db;

        public OrganizationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Organization?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _db.Organizations
                        .Include(o => o.Companies)
                        .FirstOrDefaultAsync(o => o.Id == id, ct);

        public async Task<(IReadOnlyList<Organization> Items, int Total)> ListAsync(
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.Organizations.AsQueryable();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(o => o.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(Organization organization, CancellationToken ct)
        {
            await _db.Organizations.AddAsync(organization, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Organization organization, CancellationToken ct)
        {
            _db.Organizations.Update(organization);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Organizations.FirstOrDefaultAsync(o => o.Id == id, ct);
            if (entity is null) return;

            
            _db.Organizations.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
