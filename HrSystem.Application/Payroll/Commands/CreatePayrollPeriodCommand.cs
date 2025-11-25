using AutoMapper;
using HrSystem.Application.Payroll.Abstractions;
using HrSystem.Application.Payroll.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Commands
{
    public record CreatePayrollPeriodCommand(
        int Year,
        int Month,
        DateTime FromDate,
        DateTime ToDate
    ) : IRequest<PayrollPeriodDto>;



    public class CreatePayrollPeriodHandler
       : IRequestHandler<CreatePayrollPeriodCommand, PayrollPeriodDto>
    {
        private readonly IPayrollPeriodRepository _repo;
        private readonly IMapper _mapper;

        public CreatePayrollPeriodHandler(IPayrollPeriodRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PayrollPeriodDto> Handle(CreatePayrollPeriodCommand r, CancellationToken ct)
        {
            var entity = new PayrollPeriod
            {
                Year = r.Year,
                Month = r.Month,
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                IsClosed = false
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<PayrollPeriodDto>(entity);
        }
    }
}
