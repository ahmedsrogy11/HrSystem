using FluentValidation;
using HrSystem.Application.Loans.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans
{
    public class CreateLoanRequestValidator : AbstractValidator<CreateLoanRequestCommand>
    {
        public CreateLoanRequestValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Months).GreaterThan(0);
        }
    }
}
