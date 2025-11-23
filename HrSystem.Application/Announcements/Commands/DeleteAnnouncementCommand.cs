using HrSystem.Application.Announcements.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.Commands
{
    public record DeleteAnnouncementCommand(Guid Id) : IRequest<bool>;





    public class DeleteAnnouncementHandler
    : IRequestHandler<DeleteAnnouncementCommand, bool>
    {
        private readonly IAnnouncementRepository _repo;

        public DeleteAnnouncementHandler(IAnnouncementRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteAnnouncementCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
