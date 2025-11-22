using FluentValidation;
using HrSystem.Application.Shifts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts
{
    public class CreateShiftValidator : AbstractValidator<CreateShiftCommand>
    {
        public CreateShiftValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty();
        }
    }


    public class UpdateShiftValidator : AbstractValidator<UpdateShiftCommand>
    {
        public UpdateShiftValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty();
        }
    }



    public class CreateShiftAssignmentValidator : AbstractValidator<CreateShiftAssignmentCommand>
    {
        public CreateShiftAssignmentValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId مطلوب.");

            RuleFor(x => x.ShiftId)
                .NotEmpty().WithMessage("ShiftId مطلوب.");

            RuleFor(x => x.FromDate)
                .NotEmpty().WithMessage("FromDate مطلوب.");

            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("ToDate مطلوب.")
                .GreaterThan(x => x.FromDate)
                .WithMessage("ToDate يجب أن يكون بعد FromDate.");
        }
    }


    public class UpdateShiftAssignmentValidator : AbstractValidator<UpdateShiftAssignmentCommand>
    {
        public UpdateShiftAssignmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id مطلوب.");
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId مطلوب.");
            RuleFor(x => x.ShiftId)
                .NotEmpty().WithMessage("ShiftId مطلوب.");
            RuleFor(x => x.FromDate)
                .NotEmpty().WithMessage("FromDate مطلوب.");
            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("ToDate مطلوب.")
                .GreaterThan(x => x.FromDate)
                .WithMessage("ToDate يجب أن يكون بعد FromDate.");
        }
    }
}
