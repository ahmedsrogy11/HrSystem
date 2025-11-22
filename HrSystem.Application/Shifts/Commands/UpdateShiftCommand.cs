using HrSystem.Application.Shifts.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Commands
{
    public record UpdateShiftCommand(
    Guid Id,
    string Name,
    TimeSpan StartTime,
    TimeSpan EndTime
    ) : IRequest<bool>;



    public class UpdateShiftHandler : IRequestHandler<UpdateShiftCommand, bool>
    {
        private readonly IShiftRepository _repo;

        public UpdateShiftHandler(IShiftRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateShiftCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Name = r.Name;
            entity.StartTime = r.StartTime;
            entity.EndTime = r.EndTime;

            await _repo.UpdateAsync(entity, ct);

            return true;
        }
    }
}
