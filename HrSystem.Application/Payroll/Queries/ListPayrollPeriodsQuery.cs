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
    public record ListPayrollPeriodsQuery(
        int? Year,
        int? Month,
        DateTime? FromDate,
        DateTime? ToDate,
        bool? IsClosed,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<PayrollPeriodDto>, int)>;




    public class ListPayrollPeriodsHandler
          : IRequestHandler<ListPayrollPeriodsQuery, (IReadOnlyList<PayrollPeriodDto>, int)>
    {
        private readonly IPayrollPeriodRepository _repo;
        private readonly IMapper _mapper;

        public ListPayrollPeriodsHandler(IPayrollPeriodRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<PayrollPeriodDto>, int)> Handle(
            ListPayrollPeriodsQuery r,
            CancellationToken ct)
        {
            var (entities, total) = await _repo.ListAsync(
                r.Year,
                r.Month,
                r.FromDate,
                r.ToDate,
                r.IsClosed,
                r.Page,
                r.PageSize,
                ct);

            var items = entities
                .Select(e => _mapper.Map<PayrollPeriodDto>(e))
                .ToList();

            return (items, total);
        }
    }
}



