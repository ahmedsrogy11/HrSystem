using AutoMapper;
using HrSystem.Application.Leaves.Abstractions;
using HrSystem.Application.Leaves.Dtos;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Queries
{
    public record ListLeaveRequestsQuery(
       Guid? EmployeeId,
       LeaveStatus? Status,
       int Page = 1,
       int PageSize = 20
    ) : IRequest<(IReadOnlyList<LeaveRequestDto> Items, int Total)>;



    public class ListLeaveRequestsHandler(ILeaveRequestRepository repo, IMapper mapper)
       : IRequestHandler<ListLeaveRequestsQuery, (IReadOnlyList<LeaveRequestDto>, int)>
    {
        public async Task<(IReadOnlyList<LeaveRequestDto>, int)> Handle(
            ListLeaveRequestsQuery r,
            CancellationToken ct)
        {
            var (entities, total) =
                await repo.ListAsync(r.EmployeeId, r.Status, r.Page, r.PageSize, ct);

            var items = entities.Select(mapper.Map<LeaveRequestDto>).ToList();
            return (items, total);
        }
    }
}
