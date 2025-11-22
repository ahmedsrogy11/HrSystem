using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class LeaveBalance : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        // أرصدة سنوية أساسية، ويمكن توسيعها بأنواع أخرى
        public int AnnualEntitledDays { get; set; } = 0;
        public int AnnualTakenDays { get; set; } = 0;
    }
}
