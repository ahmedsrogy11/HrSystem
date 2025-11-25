using HrSystem.Application.Payroll.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Commands
{
    public record UpdatePayrollPeriodCommand(
        Guid Id,
        int Year,
        int Month,
        DateTime FromDate,
        DateTime ToDate,
        bool IsClosed
    ) : IRequest<bool>;




    public class UpdatePayrollPeriodHandler
    : IRequestHandler<UpdatePayrollPeriodCommand, bool>
    {
        private readonly IPayrollPeriodRepository _repo;

        public UpdatePayrollPeriodHandler(IPayrollPeriodRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePayrollPeriodCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Year = r.Year;
            entity.Month = r.Month;
            entity.FromDate = r.FromDate;
            entity.ToDate = r.ToDate;
            entity.IsClosed = r.IsClosed;

            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}
