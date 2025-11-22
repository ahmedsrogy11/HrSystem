using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class OvertimeRequest : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public OvertimeStatus Status { get; set; } = OvertimeStatus.Pending;

        public Guid? ApprovedByEmployeeId { get; set; }
        public Employee? ApprovedByEmployee { get; set; }
        public DateTime? ApprovedAtUtc { get; set; }
    }
}
