using HrSystem.Application.Announcements.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.Commands
{
    public record UpdateAnnouncementCommand(
      Guid Id,
      string Title,
      string Body,
      DateTime PublishAtUtc,
      bool IsGlobal
    ) : IRequest<bool>;




    public class UpdateAnnouncementHandler
    : IRequestHandler<UpdateAnnouncementCommand, bool>
    {
        private readonly IAnnouncementRepository _repo;

        public UpdateAnnouncementHandler(IAnnouncementRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateAnnouncementCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Title = r.Title;
            entity.Body = r.Body;
            entity.PublishAtUtc = r.PublishAtUtc;
            entity.IsGlobal = r.IsGlobal;

            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}
