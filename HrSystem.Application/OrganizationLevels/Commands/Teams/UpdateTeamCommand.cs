using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Teams
{
    public record UpdateTeamCommand(
        Guid Id,
        string Name,
        string? Code,
        Guid DepartmentId,
        Guid? LeaderEmployeeId
    ) : IRequest<bool>;



    public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, bool>
    {
        private readonly ITeamRepository _repo;

        public UpdateTeamHandler(ITeamRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateTeamCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Name = r.Name;
            entity.Code = r.Code;
            entity.DepartmentId = r.DepartmentId;
            entity.LeaderEmployeeId = r.LeaderEmployeeId;

            await _repo.UpdateAsync(entity, ct);

            return true;
        }
    }
}
