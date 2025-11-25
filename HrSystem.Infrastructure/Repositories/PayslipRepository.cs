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

        public async Task AddRangeAsync(IEnumerable<Payslip> payslips, CancellationToken ct)
        {
            await _db.Payslips.AddRangeAsync(payslips, ct);
            await _db.SaveChangesAsync(ct);
        }

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

        public async Task<IReadOnlyList<Payslip>> ListByPeriodAsync(Guid payrollPeriodId, CancellationToken ct)
        {
            return await _db.Payslips
                .Include(p => p.Employee)
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .Where(p => p.PayrollPeriodId == payrollPeriodId)
                .ToListAsync(ct);   
        }
    }
}
