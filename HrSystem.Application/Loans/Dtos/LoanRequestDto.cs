using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Dtos
{
    public record LoanRequestDto(
        Guid Id,
        Guid EmployeeId,
        decimal Amount,
        int Months,
        string Status,
        Guid? ApprovedByEmployeeId,
        DateTime? ApprovedAtUtc
    );
}
