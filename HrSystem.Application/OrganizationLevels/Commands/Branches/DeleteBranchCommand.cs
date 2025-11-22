using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Branches
{
    public record DeleteBranchCommand(Guid Id) : IRequest<bool>;


    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, bool>
    {
        private readonly IBranchRepository _repo;

        public DeleteBranchHandler(IBranchRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteBranchCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
