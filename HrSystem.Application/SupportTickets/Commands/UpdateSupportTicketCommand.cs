using HrSystem.Application.SupportTickets.Abstractions;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Commands
{
    public record UpdateSupportTicketCommand(
      Guid Id,
      string Title,
      string Description,
      string? Category,
      TicketStatus Status
    ) : IRequest<bool>;





    public class UpdateSupportTicketHandler
        : IRequestHandler<UpdateSupportTicketCommand, bool>
    {
        private readonly ISupportTicketRepository _repo;
        public UpdateSupportTicketHandler(ISupportTicketRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateSupportTicketCommand r, CancellationToken ct)
        {
            var entity = await  _repo.GetByIdAsync(r.Id, ct);
            if (entity == null) return false;

            entity.Title = r.Title;
            entity.Description = r.Description;
            entity.Category = r.Category;
            entity.Status = r.Status;


            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}
       
