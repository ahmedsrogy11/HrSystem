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
   public record GetPayslipByIdQuery(Guid Id) : IRequest<PayslipDto?>;



    public class GetPayslipByIdHandler
    : IRequestHandler<GetPayslipByIdQuery, PayslipDto?>
    {
        private readonly IPayslipRepository _repo;
        private readonly IMapper _mapper;

        public GetPayslipByIdHandler(IPayslipRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PayslipDto?> Handle(GetPayslipByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<PayslipDto>(entity);
        }
    }
}
