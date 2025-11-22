using HrSystem.Application.Leaves.Abstractions;
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
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly AppDbContext _dp;

        public LeaveRequestRepository(AppDbContext dp)
        {
            _dp = dp;
        }
        public async Task AddAsync(LeaveRequest leave, CancellationToken ct)
        {
            await _dp.LeaveRequests.AddAsync(leave, ct);
            await _dp.SaveChangesAsync(ct);
        }

        public async Task<LeaveRequest?> GetByIdAsync(Guid id, CancellationToken ct)
        =>
            await _dp.LeaveRequests
                .Include(l => l.Employee)
                .Include(l =>l.ApprovedByEmployee)
                .FirstOrDefaultAsync(l => l.Id == id, ct);
        

        public async Task<(IReadOnlyList<LeaveRequest> Items, int Total)> ListAsync
            (Guid? employeeId, LeaveStatus? status, int page, int pageSize, CancellationToken ct)
        {
            var query = _dp.LeaveRequests.AsQueryable();

            if (employeeId.HasValue)
            {
                query = query.Where(l => l.EmployeeId == employeeId.Value);
            }
            if (status.HasValue)
            {
                query = query.Where(l => l.Status == status.Value);
            }
            
            var total = await query.CountAsync(ct);

            var items = await query
                .Include(l => l.Employee)
                .Include(l => l.ApprovedByEmployee)
                .OrderByDescending(l => l.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);

        }

        public async Task UpdateAsync(LeaveRequest leave, CancellationToken ct)
        {
            _dp.LeaveRequests.Update(leave);
            await _dp.SaveChangesAsync(ct);
        }
    }
}
