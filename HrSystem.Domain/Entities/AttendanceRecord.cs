using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class AttendanceRecord : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public DateTime ClockInUtc { get; set; }
        public DateTime? ClockOutUtc { get; set; }

        public bool IsLate { get; set; }
        public bool IsAbsent { get; set; }
        public string? Source { get; set; } // Biometric / Manual
    }
}
