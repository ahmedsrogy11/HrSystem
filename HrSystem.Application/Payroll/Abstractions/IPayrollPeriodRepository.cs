using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Abstractions
{
    public interface IPayrollPeriodRepository
    {
        Task<PayrollPeriod?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<PayrollPeriod> Items, int Total)> ListAsync(
           int? year,
           int? month,
           DateTime? fromDate,
           DateTime? toDate,
           bool? isClosed,
           int page,
           int pageSize,
           CancellationToken ct);

        Task AddAsync(PayrollPeriod entity, CancellationToken ct);
        Task UpdateAsync(PayrollPeriod entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
