using AutoMapper;
using HrSystem.Application.Leaves.Abstractions;
using HrSystem.Application.Leaves.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Queries
{
    public record GetLeaveRequestByIdQuery(Guid Id) : IRequest<LeaveRequestDto?>;


    public class GetLeaveRequestByIdHandler(ILeaveRequestRepository repo, IMapper mapper)
       : IRequestHandler<GetLeaveRequestByIdQuery, LeaveRequestDto?>
    {
        public async Task<LeaveRequestDto?> Handle
            (GetLeaveRequestByIdQuery r, CancellationToken ct)
        {
            var entity = await repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : mapper.Map<LeaveRequestDto>(entity);
        }
    }
}
