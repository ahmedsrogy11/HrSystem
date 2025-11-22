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
    public record ListShiftsQuery() : IRequest<IReadOnlyList<ShiftDto>>;




    public class ListShiftsHandler
    : IRequestHandler<ListShiftsQuery, IReadOnlyList<ShiftDto>>
    {
        private readonly IShiftRepository _repo;
        private readonly IMapper _mapper;

        public ListShiftsHandler(IShiftRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ShiftDto>> Handle(ListShiftsQuery request, CancellationToken ct)
        {
            var shifts = await _repo.ListAsync(ct);

            return shifts.Select(_mapper.Map<ShiftDto>).ToList();
        }
    }
}
