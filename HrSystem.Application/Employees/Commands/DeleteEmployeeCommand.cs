using HrSystem.Application.Employees.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Commands
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;




    public class DeleteEmployeeHandler(IEmployeeRepository repo)
   : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        public async Task<bool> Handle(DeleteEmployeeCommand r, CancellationToken ct)
        {
            await repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
