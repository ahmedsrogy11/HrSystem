using HrSystem.Application.Loans.Abstractions;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Commands
{
    public record ApproveLoanRequestCommand(
        Guid Id,
        Guid ApprovedByEmployeeId
    ) : IRequest<bool>;






    public class ApproveLoanRequestHandler
        : IRequestHandler<ApproveLoanRequestCommand, bool>
    {
        private readonly ILoanRequestRepository _repo;

        public ApproveLoanRequestHandler(ILoanRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(ApproveLoanRequestCommand r, CancellationToken ct)
        {
            var loan = await _repo.GetByIdAsync(r.Id, ct);
            if (loan is null) return false;

            loan.Status = LoanStatus.Approved;
            loan.ApprovedByEmployeeId = r.ApprovedByEmployeeId;
            loan.ApprovedAtUtc = DateTime.UtcNow;

            await _repo.UpdateAsync(loan, ct);

            return true;
        }
    }
}
