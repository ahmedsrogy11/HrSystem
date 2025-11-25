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
    public record GetPayrollPeriodByIdQuery(Guid Id) : IRequest<PayrollPeriodDto?>;







    public class GetPayrollPeriodByIdHandler
      : IRequestHandler<GetPayrollPeriodByIdQuery, PayrollPeriodDto?>
    {
        private readonly IPayrollPeriodRepository _repo;
        private readonly IMapper _mapper;

        public GetPayrollPeriodByIdHandler(IPayrollPeriodRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PayrollPeriodDto?> Handle(GetPayrollPeriodByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<PayrollPeriodDto>(entity);
        }
    }
}
