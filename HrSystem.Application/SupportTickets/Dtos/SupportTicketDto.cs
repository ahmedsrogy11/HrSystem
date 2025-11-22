using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Dtos
{
    public record SupportTicketDto(
      Guid Id,
      Guid EmployeeId,
      string Title,
      string Description,
      string Status,
      string? Category
    );
}
