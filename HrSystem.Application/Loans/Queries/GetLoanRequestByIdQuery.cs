using AutoMapper;
using HrSystem.Application.Attendance.Abstractions;
using HrSystem.Application.Attendance.Dtos;
using HrSystem.Application.Attendance.Queries;
using HrSystem.Application.Loans.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Queries
{
    public record GetLoanRequestByIdQuery(Guid Id) : IRequest<LoanRequestDto?>;




    public class GetLoanRequestByIdHandler
       : IRequestHandler<GetLoanRequestByIdQuery, LoanRequestDto?>
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IMapper _mapper;

        public GetLoanRequestByIdHandler(
            IAttendanceRecordRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LoanRequestDto?> Handle(GetLoanRequestByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<LoanRequestDto>(entity);
        }
    }


}
