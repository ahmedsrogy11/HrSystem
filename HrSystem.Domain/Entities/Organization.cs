using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Code { get; set; }
        public ICollection<Company> Companies { get; set; } = new Collection<Company>();
    }
}
