using HrSystem.Application.OrganizationLevels.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Departments
{
    public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;


    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IDepartmentRepository _repo;

        public DeleteDepartmentHandler(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
