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
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _db;

        public TeamRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Team?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Teams
                .Include(t => t.Department)
                .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<(IReadOnlyList<Team> Items, int Total)> ListAsync(
            Guid? departmentId,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.Teams.AsQueryable();

            if (departmentId.HasValue)
                query = query.Where(t => t.DepartmentId == departmentId.Value);

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(t => t.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddAsync(Team team, CancellationToken ct)
        {
            await _db.Teams.AddAsync(team, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Team team, CancellationToken ct)
        {
            _db.Teams.Update(team);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Teams.FirstOrDefaultAsync(t => t.Id == id, ct);
            if (entity is null) return;

            
            _db.Teams.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
