using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class PayslipEarning : BaseEntity
    {
        public Guid PayslipId { get; set; }
        public Payslip Payslip { get; set; } = default!;

        public EarningType Type { get; set; } = EarningType.BaseSalary;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
