using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Teams
{
    public record DeleteTeamCommand(Guid Id) : IRequest<bool>;

    public class DeleteTeamHandler : IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly ITeamRepository _repo;

        public DeleteTeamHandler(ITeamRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteTeamCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
