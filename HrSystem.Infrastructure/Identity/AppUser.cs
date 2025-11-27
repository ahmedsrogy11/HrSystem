using HrSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid EmployeeId { get; set; }      // ربط بالموظف
        public Employee Employee { get; set; } = default!;
    }
}
