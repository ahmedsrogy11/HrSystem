using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Payslip : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public Guid PayrollPeriodId { get; set; }
        public PayrollPeriod PayrollPeriod { get; set; } = default!;

        public decimal GrossEarnings { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }

        public ICollection<PayslipEarning> Earnings { get; set; } = new Collection<PayslipEarning>();
        public ICollection<PayslipDeduction> Deductions { get; set; } = new Collection<PayslipDeduction>();
    }
}
