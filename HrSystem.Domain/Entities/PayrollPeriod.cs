using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class PayrollPeriod : BaseEntity
    {
        public int Year { get; set; }
        public int Month { get; set; } // 1..12
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsClosed { get; set; }

        public ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
    }
}
