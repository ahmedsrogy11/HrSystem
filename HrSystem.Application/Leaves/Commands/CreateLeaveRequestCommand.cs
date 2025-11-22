using AutoMapper;
using HrSystem.Application.Leaves.Abstractions;
using HrSystem.Application.Leaves.Dtos;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Commands
{
    public record CreateLeaveRequestCommand(
       Guid EmployeeId,
       DateTime StartDate,
       DateTime EndDate,
       LeaveType Type,
       string? Reason
    ) : IRequest<LeaveRequestDto>;

    public class CreateLeaveRequestHandler(ILeaveRequestRepository repo, IMapper mapper)
        : IRequestHandler<CreateLeaveRequestCommand, LeaveRequestDto>
    {
        public async Task<LeaveRequestDto> Handle(CreateLeaveRequestCommand request, CancellationToken ct)
        {
            if (request.EndDate < request.StartDate)
                throw new InvalidOperationException("End date must be after start date.");

            var entity = new LeaveRequest
            {
                EmployeeId = request.EmployeeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Type = request.Type,
                Reason = request.Reason,
                Status = LeaveStatus.Pending
            };

            await repo.AddAsync(entity, ct);
            return mapper.Map<LeaveRequestDto>(entity);
        }
    }
}
