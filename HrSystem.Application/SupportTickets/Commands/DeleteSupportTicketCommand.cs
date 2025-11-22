using HrSystem.Application.SupportTickets.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Commands
{
    public record DeleteSupportTicketCommand(Guid Id) : IRequest<bool>;




    public class DeleteSupportTicketHandler
        : IRequestHandler<DeleteSupportTicketCommand, bool>
    {
        private readonly ISupportTicketRepository _repo;
        public DeleteSupportTicketHandler(ISupportTicketRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteSupportTicketCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
