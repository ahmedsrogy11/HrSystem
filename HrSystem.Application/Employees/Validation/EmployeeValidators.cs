using FluentValidation;
using HrSystem.Application.Employees.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Validation
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(100).WithMessage("الاسم الأول يجب ألا يتجاوز 100 حرف.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("اسم العائلة مطلوب.")
                .MaximumLength(100).WithMessage("اسم العائلة يجب ألا يتجاوز 100 حرف.");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("الرقم القومي مطلوب.")
                .MaximumLength(20).WithMessage("الرقم القومي يجب ألا يتجاوز 20 خانة.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("المسمى الوظيفي مطلوب.")
                .MaximumLength(120).WithMessage("المسمى الوظيفي يجب ألا يتجاوز 120 حرف.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("البريد الإلكتروني غير صالح.");

            RuleFor(x => x.Phone)
                .MaximumLength(30).WithMessage("رقم الهاتف يجب ألا يتجاوز 30 خانة.");

            RuleFor(x => x.BaseSalary)
                .GreaterThanOrEqualTo(0).WithMessage("الراتب الأساسي يجب أن يكون 0 أو أكبر.");

            RuleFor(x => x.SalaryCurrency)
                .NotEmpty().WithMessage("عملة الراتب مطلوبة.")
                .MaximumLength(10).WithMessage("عملة الراتب يجب ألا تتجاوز 10 خانات.");
        }
    }

    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("المعرّف (Id) مطلوب.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(100).WithMessage("الاسم الأول يجب ألا يتجاوز 100 حرف.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("اسم العائلة مطلوب.")
                .MaximumLength(100).WithMessage("اسم العائلة يجب ألا يتجاوز 100 حرف.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("المسمى الوظيفي مطلوب.")
                .MaximumLength(120).WithMessage("المسمى الوظيفي يجب ألا يتجاوز 120 حرف.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("البريد الإلكتروني غير صالح.");

            RuleFor(x => x.Phone)
                .MaximumLength(30).WithMessage("رقم الهاتف يجب ألا يتجاوز 30 خانة.");

            RuleFor(x => x.BaseSalary)
                .GreaterThanOrEqualTo(0).WithMessage("الراتب الأساسي يجب أن يكون 0 أو أكبر.");

            RuleFor(x => x.SalaryCurrency)
                .NotEmpty().WithMessage("عملة الراتب مطلوبة.")
                .MaximumLength(10).WithMessage("عملة الراتب يجب ألا تتجاوز 10 خانات.");
        }
    }
}
