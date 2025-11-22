using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Abstractions
{
    public interface ILoanRequestRepository
    {
        Task<LoanRequest?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<LoanRequest> Items, int Total)> ListAsync(
            Guid? employeeId,
            LoanStatus? status,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(LoanRequest loan, CancellationToken ct);
        Task UpdateAsync(LoanRequest loan, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
