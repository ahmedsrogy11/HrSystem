using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class LeaveRequest : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType Type { get; set; }  // Annual, Sick, Unpaid, etc.
        public string? Reason { get; set; }


        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public Guid? ApprovedByEmployeeId { get; set; } // TeamLeader/HR
        public Employee? ApprovedByEmployee { get; set; }


        public DateTime? ApprovedAtUtc { get; set; }
    }
}
