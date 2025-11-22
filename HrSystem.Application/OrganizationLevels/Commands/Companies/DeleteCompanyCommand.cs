using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Companies
{
    public record DeleteCompanyCommand(Guid Id) : IRequest<bool>;



    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repo;

        public DeleteCompanyHandler(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteCompanyCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
