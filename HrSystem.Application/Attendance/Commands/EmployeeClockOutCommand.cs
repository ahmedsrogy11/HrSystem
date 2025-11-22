using AutoMapper;
using HrSystem.Application.Attendance.Abstractions;
using HrSystem.Application.Attendance.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Commands
{
    public record EmployeeClockOutCommand(
        Guid EmployeeId,
        string? Source
    ) : IRequest<AttendanceRecordDto?>;






    public class EmployeeClockOutHandler
        : IRequestHandler<EmployeeClockOutCommand, AttendanceRecordDto?>
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeClockOutHandler(IAttendanceRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AttendanceRecordDto?> Handle
            (EmployeeClockOutCommand r, CancellationToken ct)
        {
            var existing = await _repo.GetTodayRecordForEmployeeAsync(r.EmployeeId, ct);

            if (existing is null)
            {
                return null;
            }

            if (existing.ClockOutUtc is not null)
            {
                return _mapper.Map<AttendanceRecordDto?>(existing);
            }

            existing.ClockOutUtc = DateTime.UtcNow;
            existing.Source = r.Source;

            await _repo.UpdateAsync(existing, ct);

            return _mapper.Map<AttendanceRecordDto?>(existing);
        }
    }
}
