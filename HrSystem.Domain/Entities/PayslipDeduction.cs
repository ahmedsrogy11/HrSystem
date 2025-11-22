using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class PayslipDeduction : BaseEntity
    {
        public Guid PayslipId { get; set; }
        public Payslip Payslip { get; set; } = default!;

        public DeductionType Type { get; set; } = DeductionType.Other;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
