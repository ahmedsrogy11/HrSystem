using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Code { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; } = default!;

        public ICollection<Branch> Branches { get; set; } = new Collection<Branch>();
    }
}
