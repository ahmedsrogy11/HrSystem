using AutoMapper;
using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Application.Overtime.Dto;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Commands
{
    public record CreateOvertimeRequestCommand(
        Guid EmployeeId,
        DateTime Date,
        decimal Hours
    ) : IRequest<OvertimeRequestDto>;






    public class CreateOvertimeRequestHandler
        : IRequestHandler<CreateOvertimeRequestCommand, OvertimeRequestDto>
    {
        private readonly IOvertimeRequestRepository _repo;
        private readonly IMapper _mapper;

        public CreateOvertimeRequestHandler(IOvertimeRequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OvertimeRequestDto> Handle(CreateOvertimeRequestCommand r, CancellationToken ct)
        {
            var entity = new OvertimeRequest
            {
                EmployeeId = r.EmployeeId,
                Date = r.Date,
                Hours = r.Hours,
                Status = OvertimeStatus.Pending
            };

            await _repo.AddAsync(entity, ct);
            return _mapper.Map<OvertimeRequestDto>(entity);
        }
    }

}
