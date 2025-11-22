using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Companies
{
    public record UpdateCompanyCommand(
        Guid Id,
        string Name,
        string? Code,
        Guid OrganizationId
    ) : IRequest<bool>;


    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repo;

        public UpdateCompanyHandler(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateCompanyCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Name = r.Name;
            entity.Code = r.Code;
            entity.OrganizationId = r.OrganizationId;

            await _repo.UpdateAsync(entity, ct);

            return true;
        }
    }
}
