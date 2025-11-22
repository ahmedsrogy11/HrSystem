using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Abstractions
{
    public interface ISupportTicketRepository
    {
        Task<SupportTicket?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<(IReadOnlyList<SupportTicket> Items, int Total)> ListAsync(
            Guid? employeeId,
            TicketStatus? status,
            string? category,
            int page,
            int pageSize,
            CancellationToken ct);

        Task AddAsync(SupportTicket entity, CancellationToken ct);
        Task UpdateAsync(SupportTicket entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
