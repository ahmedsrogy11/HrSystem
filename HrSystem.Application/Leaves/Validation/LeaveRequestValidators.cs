using FluentValidation;
using HrSystem.Application.Leaves.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Validation
{
    // ✅ التحقق من إنشاء طلب إجازة
    public class CreateLeaveRequestValidator : AbstractValidator<CreateLeaveRequestCommand>
    {
        public CreateLeaveRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("الموظف مطلوب.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("نوع الإجازة غير صالح.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("تاريخ بداية الإجازة مطلوب.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("تاريخ نهاية الإجازة مطلوب.");

            // نهاية الإجازة بعد أو مساوية للبداية
            RuleFor(x => x)
                .Must(x => x.EndDate.Date >= x.StartDate.Date)
                .WithMessage("تاريخ نهاية الإجازة يجب أن يكون بعد أو يساوي تاريخ البداية.");

            // مدة الإجازة لا تزيد مثلاً عن 30 يوم (تقدر تغير الرقم)
            RuleFor(x => x)
                .Must(x => (x.EndDate.Date - x.StartDate.Date).TotalDays <= 30)
                .WithMessage("مدة الإجازة لا يجب أن تتجاوز 30 يومًا.")
                .When(x => x.EndDate.Date >= x.StartDate.Date);

            RuleFor(x => x.Reason)
                .MaximumLength(500).WithMessage("سبب الإجازة يجب ألا يتجاوز 500 حرف.");
        }
    }

    // ✅ الموافقة على الإجازة
    public class ApproveLeaveRequestValidator : AbstractValidator<ApproveLeaveRequestCommand>
    {
        public ApproveLeaveRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف طلب الإجازة مطلوب.");

            RuleFor(x => x.ApproverEmployeeId)
                .NotEmpty().WithMessage("معرّف الموظف الموافق مطلوب.");
        }
    }

    // ✅ رفض الإجازة
    public class RejectLeaveRequestValidator : AbstractValidator<RejectLeaveRequestCommand>
    {
        public RejectLeaveRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف طلب الإجازة مطلوب.");

            RuleFor(x => x.ApproverEmployeeId)
                .NotEmpty().WithMessage("معرّف الموظف الرافض مطلوب.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("سبب الرفض مطلوب.")
                .MaximumLength(500).WithMessage("سبب الرفض يجب ألا يتجاوز 500 حرف.");
        }
    }
}
