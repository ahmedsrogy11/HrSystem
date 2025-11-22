using AutoMapper;
using HrSystem.Application.Attendance.Abstractions;
using HrSystem.Application.Attendance.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Commands
{
    public record CreateAttendanceRecordCommand(
        Guid EmployeeId,
        DateTime ClockInUtc,
        DateTime? ClockOutUtc,
        bool IsLate,
        bool IsAbsent,
        string? Source
    ) : IRequest<AttendanceRecordDto>;



    public class CreateAttendanceRecordHandler
       : IRequestHandler<CreateAttendanceRecordCommand, AttendanceRecordDto>
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IMapper _mapper;

        public CreateAttendanceRecordHandler(IAttendanceRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AttendanceRecordDto> Handle
            (CreateAttendanceRecordCommand r, CancellationToken ct)
        {
            var entity = new AttendanceRecord
            {
                EmployeeId = r.EmployeeId,
                ClockInUtc = r.ClockInUtc,
                ClockOutUtc = r.ClockOutUtc,
                IsLate = r.IsLate,
                IsAbsent = r.IsAbsent,
                Source = r.Source
            };

            await _repo.AddAsync(entity , ct);
            return _mapper.Map<AttendanceRecordDto>(entity);
        }
    }
}
