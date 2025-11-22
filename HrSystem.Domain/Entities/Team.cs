using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Code { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;

        public Guid? LeaderEmployeeId { get; set; } // اختيارية
        public Employee? LeaderEmployee { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
    }
}
