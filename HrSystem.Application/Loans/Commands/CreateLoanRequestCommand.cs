using AutoMapper;
using HrSystem.Application.Loans.Abstractions;
using HrSystem.Application.Loans.Dtos;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Commands
{
    public record CreateLoanRequestCommand(
       Guid EmployeeId,
       decimal Amount,
       int Months
    ) : IRequest<LoanRequestDto>;






    public class CreateLoanRequestHandler
       : IRequestHandler<CreateLoanRequestCommand, LoanRequestDto>
    {
        private readonly ILoanRequestRepository _repo;
        private readonly IMapper _mapper;

        public CreateLoanRequestHandler(ILoanRequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LoanRequestDto> Handle(CreateLoanRequestCommand r, CancellationToken ct)
        {
            var loan = new LoanRequest
            {
                EmployeeId = r.EmployeeId,
                Amount = r.Amount,
                Months = r.Months,
                Status = LoanStatus.Pending
            };

            await _repo.AddAsync(loan, ct);

            return _mapper.Map<LoanRequestDto>(loan);
        }
    }
}
