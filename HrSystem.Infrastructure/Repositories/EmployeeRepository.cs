using HrSystem.Application.Employees.Abstractions;
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
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly AppDbContext _db;
        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Employee employee, CancellationToken ct)
        {
            await _db.Employees.AddAsync(employee, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id, ct);
            if (entity is null)
                return;

          
            _db.Employees.Remove(entity);
            await _db.SaveChangesAsync(ct);

        }

        public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct)
        {
           return await _db.Employees.FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<(IReadOnlyList<Employee> Items, int Total)> ListAsync(int page, int pageSize, CancellationToken ct)
        {
            var skip = (page - 1)*pageSize;
            var total = await _db.Employees.CountAsync(ct);
            var itmes = await _db.Employees
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(ct);
            return (itmes, total);


        }

        public async Task<bool> NationalIdExistsAsync(string nationalId, CancellationToken ct)
        {
            return await _db.Employees.AnyAsync(e => e.NationalId == nationalId, ct);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken ct)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync(ct);
        }
    }
}
