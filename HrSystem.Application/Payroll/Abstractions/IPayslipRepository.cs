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
        Task<Payslip?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<Payslip> Items, int Total)> ListByPeriodAsync(
            Guid payrollPeriodId,
            int page,
            int pageSize,
            CancellationToken ct);

        Task<(IReadOnlyList<Payslip> Items, int Total)> ListByEmployeeAsync(
            Guid employeeId,
            Guid? payrollPeriodId,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddRangeAsync(IEnumerable<Payslip> payslips, CancellationToken ct);

        Task DeleteByPeriodAsync(Guid payrollPeriodId, CancellationToken ct);
    }
}
