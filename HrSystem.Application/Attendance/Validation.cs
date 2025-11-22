using FluentValidation;
using HrSystem.Application.Attendance.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance
{
    public class CreateAttendanceRecordValidator : AbstractValidator<CreateAttendanceRecordCommand>
    {
        public CreateAttendanceRecordValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty();

            RuleFor(x => x.ClockInUtc)
                .NotEmpty();

            RuleFor(x => x.Source)
                .MaximumLength(50);
        }
    }

    public class UpdateAttendanceRecordValidator : AbstractValidator<UpdateAttendanceRecordCommand>
    {
        public UpdateAttendanceRecordValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.ClockInUtc)
                .NotEmpty();

            RuleFor(x => x.Source)
                .MaximumLength(50);
        }
    }




    public class EmployeeClockInValidator : AbstractValidator<EmployeeClockInCommand>
    {
        public EmployeeClockInValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty();

            RuleFor(x => x.Source)
                .MaximumLength(50);
        }
    }

    public class EmployeeClockOutValidator : AbstractValidator<EmployeeClockOutCommand>
    {
        public EmployeeClockOutValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty();

            RuleFor(x => x.Source)
                .MaximumLength(50);
        }
    }
}
