using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Code { get; set; }
        public string? Address { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = default!;

        public ICollection<Department> Departments { get; set; } = new Collection<Department>();
    }
}
