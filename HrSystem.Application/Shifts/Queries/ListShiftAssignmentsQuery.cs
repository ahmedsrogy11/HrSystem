using AutoMapper;
using HrSystem.Application.Shifts.Abstractions;
using HrSystem.Application.Shifts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Queries
{
    public record ListShiftAssignmentsQuery(
        Guid? EmployeeId,
        Guid? ShiftId,
        DateTime? DateFrom,
        DateTime? DateTo,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<ShiftAssignmentDto>, int)>;






    public class ListShiftAssignmentsHandler
       : IRequestHandler<ListShiftAssignmentsQuery, (IReadOnlyList<ShiftAssignmentDto>, int)>
    {
        private readonly IShiftAssignmentRepository _repo;
        private readonly IMapper _mapper;

        public ListShiftAssignmentsHandler(IShiftAssignmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<ShiftAssignmentDto>, int)> Handle(
            ListShiftAssignmentsQuery r,
            CancellationToken ct)
        {
            var (entities, total) = await _repo.ListAsync(
                r.EmployeeId,
                r.ShiftId,
                r.DateFrom,
                r.DateTo,
                r.Page,
                r.PageSize,
                ct);

            var dtos = entities
                .Select(x => _mapper.Map<ShiftAssignmentDto>(x))
                .ToList();

            return (dtos, total);
        }
    }

}
