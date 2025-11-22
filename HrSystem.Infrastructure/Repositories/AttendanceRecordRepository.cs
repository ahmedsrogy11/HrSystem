using HrSystem.Application.Attendance.Abstractions;
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
    public class AttendanceRecordRepository : IAttendanceRecordRepository
    {
        private readonly AppDbContext _db;

        public AttendanceRecordRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AttendanceRecord?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.AttendanceRecords
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task<(IReadOnlyList<AttendanceRecord> Items, int Total)> ListAsync(
            Guid? employeeId,
            DateTime? dateFrom,
            DateTime? dateTo,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _db.AttendanceRecords.AsQueryable();
            if (employeeId.HasValue)
            {
                query = query.Where(a => a.EmployeeId == employeeId.Value);
            }

            if (dateFrom.HasValue)
            {
                query = query.Where(a => a.ClockInUtc >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(a => a.ClockInUtc <= dateTo.Value);
            }

            var total = await query.CountAsync(ct);

            var items = await query
                .Include(a => a.Employee)
                .OrderByDescending(a => a.ClockInUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);

        }

        public async Task AddAsync(AttendanceRecord record, CancellationToken ct)
        {
            await _db.AttendanceRecords.AddAsync(record, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(AttendanceRecord record, CancellationToken ct)
        {
            _db.AttendanceRecords.Update(record);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.AttendanceRecords.FirstOrDefaultAsync(a => a.Id == id, ct);
            if (entity is null) return;

            _db.AttendanceRecords.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<AttendanceRecord?> GetTodayRecordForEmployeeAsync(Guid employeeId, CancellationToken ct)
        {
            var today = DateTime.UtcNow.Date;

            return await _db.AttendanceRecords
                .Where(a => a.EmployeeId == employeeId && a.ClockInUtc.Date == today)
                .OrderByDescending(a => a.ClockInUtc)
                .FirstOrDefaultAsync(ct);


        }
    }
}
