using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Dtos
{
    public record ShiftDto(
        Guid Id,
        string Name,
        TimeSpan StartTime,
        TimeSpan EndTime,
        bool IsOvernight
    );


}
