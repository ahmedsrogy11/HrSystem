using HrSystem.Application.Leaves.Abstractions;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Commands
{
    public record RejectLeaveRequestCommand(
         Guid Id,
         Guid ApproverEmployeeId,
         string? Reason
    ) : IRequest<bool>;

    public class RejectLeaveRequestHandler(ILeaveRequestRepository repo)
        : IRequestHandler<RejectLeaveRequestCommand, bool>
    {
        public async Task<bool> Handle
            (RejectLeaveRequestCommand r, CancellationToken ct)
        {
            var leaveRequest = await repo.GetByIdAsync(r.Id, ct);

            if (leaveRequest is null)
                return false;


            if (leaveRequest.Status != LeaveStatus.Pending)
                return false;

            leaveRequest.Status = LeaveStatus.Rejected;
            leaveRequest.ApprovedByEmployeeId = r.ApproverEmployeeId;
            leaveRequest.ApprovedAtUtc = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(r.Reason))
                leaveRequest.Reason = r.Reason;

            await repo.UpdateAsync(leaveRequest, ct);
            return true;

        }
    }
}
