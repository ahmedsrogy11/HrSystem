using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Branches
{
    public record UpdateBranchCommand(
        Guid Id,
        string Name,
        string? Code,
        string? Address,
        Guid CompanyId
    ) : IRequest<bool>;



    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, bool>
    {
        private readonly IBranchRepository _repo;

        public UpdateBranchHandler(IBranchRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateBranchCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null)
                return false;

            entity.Name = r.Name;
            entity.Code = r.Code;
            entity.Address = r.Address;
            entity.CompanyId = r.CompanyId;

            await _repo.UpdateAsync(entity, ct);

            return true;
        }
    }
}
