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
    public record EmployeeClockInCommand(
        Guid EmployeeId,
        string? Source
    ) : IRequest<AttendanceRecordDto>;






    public class EmployeeClockInHandler
        : IRequestHandler<EmployeeClockInCommand, AttendanceRecordDto>
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeClockInHandler(IAttendanceRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AttendanceRecordDto> Handle(EmployeeClockInCommand r, CancellationToken ct)
        {
            // لو سجل حضور قبل كده النهارده → رجّع نفس الريكورد
            var existing = await _repo.GetTodayRecordForEmployeeAsync(r.EmployeeId, ct);
            if (existing is not null)
                return _mapper.Map<AttendanceRecordDto>(existing);

            var now = DateTime.UtcNow;

            var entity = new AttendanceRecord
            {
                EmployeeId = r.EmployeeId,
                ClockInUtc = now,
                ClockOutUtc = null,
                IsAbsent = false,
                IsLate = false, 
                Source = r.Source
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<AttendanceRecordDto>(entity);
        }
    }
}
