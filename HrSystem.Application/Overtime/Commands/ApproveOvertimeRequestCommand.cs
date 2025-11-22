using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Commands
{
    public record ApproveOvertimeRequestCommand(
         Guid Id,
         Guid ApprovedByEmployeeId
    ) : IRequest<bool>;







    public class ApproveOvertimeRequestHandler
        : IRequestHandler<ApproveOvertimeRequestCommand, bool>
    {
        private readonly IOvertimeRequestRepository _repo;

        public ApproveOvertimeRequestHandler(IOvertimeRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle
            (ApproveOvertimeRequestCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.Status = OvertimeStatus.Approved;
            entity.ApprovedByEmployeeId = r.ApprovedByEmployeeId;
            entity.ApprovedAtUtc = DateTime.UtcNow;

            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }





}
