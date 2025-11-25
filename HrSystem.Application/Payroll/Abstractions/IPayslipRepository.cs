using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Abstractions
{
    public interface IPayslipRepository
    {
        Task<IReadOnlyList<Payslip>> ListByPeriodAsync(Guid payrollPeriodId, CancellationToken ct);
        Task AddRangeAsync(IEnumerable<Payslip> payslips, CancellationToken ct);
        Task DeleteByPeriodAsync(Guid payrollPeriodId, CancellationToken ct);
    }
}
