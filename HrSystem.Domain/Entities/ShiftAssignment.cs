using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class ShiftAssignment : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public Guid ShiftId { get; set; }
        public Shift Shift { get; set; } = default!;

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
