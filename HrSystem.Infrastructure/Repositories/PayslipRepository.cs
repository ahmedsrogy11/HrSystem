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
    public class PayslipRepository : IPayslipRepository
    {
        private readonly AppDbContext _db;

        public PayslipRepository(AppDbContext db)
        {
            _db = db;
        }

        // Adds a range of payslips to the database asynchronously.
        public async Task AddRangeAsync(IEnumerable<Payslip> payslips, CancellationToken ct)
        {
            await _db.Payslips.AddRangeAsync(payslips, ct);
            await _db.SaveChangesAsync(ct);
        }


        // Deletes payslips associated with a specific payroll period asynchronously.

        public async Task DeleteByPeriodAsync(Guid payrollPeriodId, CancellationToken ct)
        {
            var exsitingPayslips = await _db.Payslips
                 .Where(p => p.PayrollPeriodId == payrollPeriodId)
                 .ToListAsync(ct);

            if (!exsitingPayslips.Any())
                return;

            _db.Payslips.RemoveRange(exsitingPayslips);
             await _db.SaveChangesAsync(ct);
        }



        // Retrieves a payslip by its unique identifier asynchronously.
        public Task<Payslip?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return _db.Payslips
                .Include(p => p.Employee)
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }




        // Lists payslips for a specific employee, optionally filtered by payroll period, with pagination support.
        public async Task<(IReadOnlyList<Payslip> Items, int Total)> ListByEmployeeAsync
            (Guid employeeId,
            Guid? payrollPeriodId, 
            int page,
            int pageSize, 
            CancellationToken ct)
        {
            
            var query =  _db.Payslips
                .Include(p => p.Employee)
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .Where(p => p.EmployeeId == employeeId);

            if (payrollPeriodId.HasValue)
                query = query.Where(p => p.PayrollPeriodId == payrollPeriodId.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(p => p.PayrollPeriod.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);


        }






        // Lists payslips for a specific payroll period with pagination support.
        public async Task<(IReadOnlyList<Payslip> Items, int Total)> ListByPeriodAsync
            (Guid payrollPeriodId, 
            int page,
            int pageSize, 
            CancellationToken ct)
        {
           var query =  _db.Payslips
                .Include(p => p.Employee)
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .Where(p => p.PayrollPeriodId == payrollPeriodId);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(p => p.Employee.FirstName)
                .ThenByDescending(p => p.Employee.LastName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);

        }


    }
}
