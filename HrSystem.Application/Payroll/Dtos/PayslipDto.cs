using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Dtos
{
    public class PayslipDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = default!;

        public Guid PayrollPeriodId { get; set; }

        public decimal GrossEarnings { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }

        public IReadOnlyList<PayslipEarningItemDto> Earnings { get; set; } = new List<PayslipEarningItemDto>();
        public IReadOnlyList<PayslipDeductionItemDto> Deductions { get; set; } = new List<PayslipDeductionItemDto>();
    }

    public class PayslipEarningItemDto
    {
        public string Type { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }

    public class PayslipDeductionItemDto
    {
        public string Type { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
