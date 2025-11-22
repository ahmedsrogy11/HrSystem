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
    public record GetAttendanceRecordByIdQuery(Guid Id) : IRequest<AttendanceRecordDto?>;






    public class GetAttendanceRecordByIdHandler
       : IRequestHandler<GetAttendanceRecordByIdQuery, AttendanceRecordDto?>
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IMapper _mapper;

        public GetAttendanceRecordByIdHandler(
            IAttendanceRecordRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AttendanceRecordDto?> Handle(GetAttendanceRecordByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<AttendanceRecordDto>(entity);
        }
    }
}
