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
    public record GetShiftByIdQuery(Guid Id) : IRequest<ShiftDto?>;





    public class GetShiftByIdHandler
    : IRequestHandler<GetShiftByIdQuery, ShiftDto?>
    {
        private readonly IShiftRepository _repo;
        private readonly IMapper _mapper;

        public GetShiftByIdHandler(IShiftRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ShiftDto?> Handle(GetShiftByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<ShiftDto>(entity);
        }
    }

}
