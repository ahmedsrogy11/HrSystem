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
    public record ApproveLeaveRequestCommand(
         Guid Id,
         Guid ApproverEmployeeId
    ) : IRequest<bool>;


    public class ApproveLeaveRequestHandler(ILeaveRequestRepository repo)
       : IRequestHandler<ApproveLeaveRequestCommand, bool>
    {
        public async Task<bool> Handle(ApproveLeaveRequestCommand r, CancellationToken ct)
        {
            var leaveRequest = await repo.GetByIdAsync(r.Id, ct);

            if (leaveRequest is null)
                return false;

            if (leaveRequest.Status != LeaveStatus.Pending)
                return false;

           leaveRequest.Status = LeaveStatus.Approved;
              leaveRequest.ApprovedByEmployeeId = r.ApproverEmployeeId;
                leaveRequest.ApprovedAtUtc = DateTime.UtcNow;

            await repo.UpdateAsync(leaveRequest, ct);
            return true;
        }
    }
}
