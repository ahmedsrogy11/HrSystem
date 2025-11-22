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
    public record GetShiftAssignmentByIdQuery(Guid Id) : IRequest<ShiftAssignmentDto?>;




    public class GetShiftAssignmentByIdHandler
       : IRequestHandler<GetShiftAssignmentByIdQuery, ShiftAssignmentDto?>
    {
        private readonly IShiftAssignmentRepository _repo;
        private readonly IMapper _mapper;
        public GetShiftAssignmentByIdHandler(IShiftAssignmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ShiftAssignmentDto?> Handle(GetShiftAssignmentByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<ShiftAssignmentDto>(entity);
        }
    }
}
