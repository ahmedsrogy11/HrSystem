using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Organizations
{
    public record UpdateOrganizationCommand(
        Guid Id,
        string Name,
        string? Code
    ) : IRequest<bool>;


    public class UpdateOrganizationHandler : IRequestHandler<UpdateOrganizationCommand, bool>
    {
        private readonly IOrganizationRepository _repo;

        public UpdateOrganizationHandler(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateOrganizationCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Name = r.Name;
            entity.Code = r.Code;

            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}
