using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class LoanRequest : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public decimal Amount { get; set; }
        public int Months { get; set; } // مدة السداد
        public LoanStatus Status { get; set; } = LoanStatus.Pending;


        public Guid? ApprovedByEmployeeId { get; set; }
        public Employee? ApprovedByEmployee { get; set; }
        public DateTime? ApprovedAtUtc { get; set; }
    }
}
