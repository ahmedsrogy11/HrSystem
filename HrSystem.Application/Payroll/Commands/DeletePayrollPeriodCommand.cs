using HrSystem.Application.Payroll.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Commands
{
    public record DeletePayrollPeriodCommand(Guid Id) : IRequest<bool>;






    public class DeletePayrollPeriodHandler
        : IRequestHandler<DeletePayrollPeriodCommand, bool>
    {
        private readonly IPayrollPeriodRepository _repo;

        public DeletePayrollPeriodHandler(IPayrollPeriodRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePayrollPeriodCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }

}
