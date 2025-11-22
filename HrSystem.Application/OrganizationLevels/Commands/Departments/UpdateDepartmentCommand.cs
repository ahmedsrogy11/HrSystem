using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Departments
{
    public record UpdateDepartmentCommand(
       Guid Id,
       string Name,
       string? Code,
       Guid BranchId
   ) : IRequest<bool>;

    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, bool>
    {
        private readonly IDepartmentRepository _repo;

        public UpdateDepartmentHandler(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateDepartmentCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Name = r.Name;
            entity.Code = r.Code;
            entity.BranchId = r.BranchId;

            await _repo.UpdateAsync(entity, ct);

            return true;
        }
    }
}
