using FluentValidation;
using HrSystem.Application.Overtime.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime
{
    public class CreateOvertimeRequestValidator
     : AbstractValidator<CreateOvertimeRequestCommand>
    {
        public CreateOvertimeRequestValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Hours).GreaterThan(0)
                .WithMessage("عدد الساعات يجب أن يكون أكبر من 0");
        }
    }



    public class ApproveOvertimeRequestValidator
    : AbstractValidator<ApproveOvertimeRequestCommand>
    {
        public ApproveOvertimeRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ApprovedByEmployeeId).NotEmpty();
        }
    }
}
