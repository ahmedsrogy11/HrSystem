using AutoMapper;
using HrSystem.Application.Attendance.Abstractions;
using HrSystem.Application.Attendance.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Attendance.Queries
{
    public record ListAttendanceRecordsQuery(
        Guid? EmployeeId,
        DateTime? DateFrom,
        DateTime? DateTo,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<AttendanceRecordDto> Items, int Total)>;




    public class ListAttendanceRecordsHandler
      : IRequestHandler<ListAttendanceRecordsQuery, (IReadOnlyList<AttendanceRecordDto>, int)>
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IMapper _mapper;

        public ListAttendanceRecordsHandler(
            IAttendanceRecordRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<AttendanceRecordDto>, int)> Handle(
            ListAttendanceRecordsQuery r,
            CancellationToken ct)
        {
            var (entities, total) = await _repo.ListAsync(
                r.EmployeeId,
                r.DateFrom,
                r.DateTo,
                r.Page,
                r.PageSize,
                ct);

            var dtos = entities.Select(_mapper.Map<AttendanceRecordDto>).ToList();
            return (dtos, total);
        }
    }
}
