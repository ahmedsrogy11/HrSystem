using FluentValidation;
using HrSystem.Application.Payroll.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll
{
    public class CreatePayrollPeriodValidator : AbstractValidator<CreatePayrollPeriodCommand>
    {
        public CreatePayrollPeriodValidator()
        {
            RuleFor(x => x.Year).InclusiveBetween(2000, 2100);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);

            RuleFor(x => x.FromDate).NotEmpty();
            RuleFor(x => x.ToDate)
                .GreaterThan(x => x.FromDate)
                .WithMessage("ToDate must be after FromDate.");
        }
    }





    public class UpdatePayrollPeriodValidator : AbstractValidator<UpdatePayrollPeriodCommand>
    {
        public UpdatePayrollPeriodValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Year).InclusiveBetween(2000, 2100);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);

            RuleFor(x => x.FromDate).NotEmpty();
            RuleFor(x => x.ToDate)
                .GreaterThan(x => x.FromDate);
        }
    }


    public class GeneratePayrollForPeriodValidator : AbstractValidator<GeneratePayrollForPeriodCommand>
    {
        public GeneratePayrollForPeriodValidator()
        {
            RuleFor(x => x.PayrollPeriodId).NotEmpty();
        }
    }

}
