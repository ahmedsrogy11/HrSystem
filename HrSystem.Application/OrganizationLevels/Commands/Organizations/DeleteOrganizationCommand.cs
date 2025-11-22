using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Organizations
{
    public record DeleteOrganizationCommand(Guid Id) : IRequest<bool>;

    public class DeleteOrganizationHandler : IRequestHandler<DeleteOrganizationCommand, bool>
    {
        private readonly IOrganizationRepository _repo;

        public DeleteOrganizationHandler(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteOrganizationCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
