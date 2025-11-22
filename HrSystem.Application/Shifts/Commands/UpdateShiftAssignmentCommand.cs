using HrSystem.Application.Shifts.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Commands
{
    public record UpdateShiftAssignmentCommand(
        Guid Id,
        Guid EmployeeId,
        Guid ShiftId,
        DateTime FromDate,
        DateTime ToDate
    ): IRequest<bool>;





    public class UpdateShiftAssignmentHandler
        : IRequestHandler<UpdateShiftAssignmentCommand, bool>
    {
        private readonly IShiftAssignmentRepository _repo;

        public UpdateShiftAssignmentHandler(IShiftAssignmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateShiftAssignmentCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null)
                return false;

            entity.EmployeeId = r.EmployeeId;
            entity.ShiftId = r.ShiftId;
            entity.FromDate = r.FromDate;
            entity.ToDate = r.ToDate;

            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }




}
