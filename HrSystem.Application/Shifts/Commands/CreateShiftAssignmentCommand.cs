using AutoMapper;
using HrSystem.Application.Shifts.Abstractions;
using HrSystem.Application.Shifts.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Commands
{
    public record CreateShiftAssignmentCommand(
    Guid EmployeeId,
    Guid ShiftId,
    DateTime FromDate,
    DateTime ToDate
    ) : IRequest<ShiftAssignmentDto>;





    public class CreateShiftAssignmentHandler
    : IRequestHandler<CreateShiftAssignmentCommand, ShiftAssignmentDto>
    {
        private readonly IShiftAssignmentRepository _repo;
        private readonly IMapper _mapper;

        public CreateShiftAssignmentHandler(IShiftAssignmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ShiftAssignmentDto> Handle(CreateShiftAssignmentCommand r, CancellationToken ct)
        {
            var entity = new ShiftAssignment
            {
                EmployeeId = r.EmployeeId,
                ShiftId = r.ShiftId,
                FromDate = r.FromDate,
                ToDate = r.ToDate
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<ShiftAssignmentDto>(entity);
        }
    }
}
