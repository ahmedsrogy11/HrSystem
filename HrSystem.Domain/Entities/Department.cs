using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Code { get; set; }

        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = default!;

        public ICollection<Team> Teams { get; set; } = new Collection<Team>();
    }
}
