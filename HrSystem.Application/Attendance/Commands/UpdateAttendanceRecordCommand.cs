using HrSystem.Application.Attendance.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Commands
{
    public record UpdateAttendanceRecordCommand(
       Guid Id,
       DateTime ClockInUtc,
       DateTime? ClockOutUtc,
       bool IsLate,
       bool IsAbsent,
       string? Source
    ) : IRequest<bool>;





    public class UpdateAttendanceRecordHandler
        : IRequestHandler<UpdateAttendanceRecordCommand, bool>
    {
        private readonly IAttendanceRecordRepository _repo;

        public UpdateAttendanceRecordHandler(IAttendanceRecordRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateAttendanceRecordCommand r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            if (entity is null) return false;

            entity.ClockInUtc = r.ClockInUtc;
            entity.ClockOutUtc = r.ClockOutUtc;
            entity.IsLate = r.IsLate;
            entity.IsAbsent = r.IsAbsent;
            entity.Source = r.Source;

            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}