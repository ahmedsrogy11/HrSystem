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
    public record CreateShiftCommand(
    string Name,
    TimeSpan StartTime,
    TimeSpan EndTime
    )    : IRequest<ShiftDto>;





    public class CreateShiftHandler : IRequestHandler<CreateShiftCommand, ShiftDto>
    {
        private readonly IShiftRepository _repo;
        private readonly IMapper _mapper;

        public CreateShiftHandler(IShiftRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ShiftDto> Handle(CreateShiftCommand r, CancellationToken ct)
        {
            var entity = new Shift
            {
                Name = r.Name,
                StartTime = r.StartTime,
                EndTime = r.EndTime
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<ShiftDto>(entity);
        }
    }
}
