using AutoMapper;
using HrSystem.Application.Payroll.Abstractions;
using HrSystem.Application.Payroll.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Queries
{
    public record GetPayslipsByEmployeeQuery(
     Guid EmployeeId,
     Guid? PayrollPeriodId,
     int Page = 1,
     int PageSize = 20
    ) : IRequest<(IReadOnlyList<PayslipDto> Items, int Total)>;




    public class GetPayslipsByEmployeeHandler
    : IRequestHandler<GetPayslipsByEmployeeQuery, (IReadOnlyList<PayslipDto> Items, int Total)>
    {
        private readonly IPayslipRepository _repo;
        private readonly IMapper _mapper;

        public GetPayslipsByEmployeeHandler(IPayslipRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<PayslipDto> Items, int Total)> Handle(
            GetPayslipsByEmployeeQuery r, CancellationToken ct)
        {
            var (entities, total) = await _repo.ListByEmployeeAsync(
                r.EmployeeId, r.PayrollPeriodId, r.Page, r.PageSize, ct);

            var items = entities
                .Select(e => _mapper.Map<PayslipDto>(e))
                .ToList();

            return (items, total);
        }
    }



}
