using HrSystem.Application.Attendance.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Commands
{
    public record DeleteAttendanceRecordCommand(Guid Id) : IRequest<bool>;






    public class DeleteAttendanceRecordHandler
       : IRequestHandler<DeleteAttendanceRecordCommand, bool>
    {
        private readonly IAttendanceRecordRepository _repo;

        public DeleteAttendanceRecordHandler(IAttendanceRecordRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteAttendanceRecordCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
