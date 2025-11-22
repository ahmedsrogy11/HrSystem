using HrSystem.Application.Employees.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Commands
{
    public record UpdateEmployeeCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    string JobTitle,
    decimal BaseSalary,
    string SalaryCurrency
    ) : IRequest<bool>;



    public class UpdateEmployeeHandler(IEmployeeRepository repo)
   : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        public async Task<bool> Handle(UpdateEmployeeCommand r, CancellationToken ct)
        {
            var e = await repo.GetByIdAsync(r.Id, ct);
            if (e is null) return false;

            e.FirstName = r.FirstName;
            e.LastName = r.LastName;
            e.Email = r.Email;
            e.Phone = r.Phone;
            e.JobTitle = r.JobTitle;
            e.BaseSalary = r.BaseSalary;
            e.SalaryCurrency = r.SalaryCurrency;

            await repo.UpdateAsync(e, ct);
            return true;
        }
    }
}
