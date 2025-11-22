using AutoMapper;
using HrSystem.Application.Loans.Abstractions;
using HrSystem.Application.Loans.Dtos;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Queries
{
    public record ListLoanRequestsQuery(
        Guid? EmployeeId,
        LoanStatus? Status,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<LoanRequestDto>, int)>;




    public class ListLoanRequestsHandler
       : IRequestHandler<ListLoanRequestsQuery, (IReadOnlyList<LoanRequestDto>, int)>
    {
        private readonly ILoanRequestRepository _repo;
        private readonly IMapper _mapper;

        public ListLoanRequestsHandler(ILoanRequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<LoanRequestDto>, int)> Handle(
            ListLoanRequestsQuery r,
            CancellationToken ct)
        {
            var (entities, total) = await _repo.ListAsync(
                r.EmployeeId,
                r.Status,
                r.Page,
                r.PageSize,
                ct);

            var dtos = entities
                .Select(e => _mapper.Map<LoanRequestDto>(e))
                .ToList();

            return (dtos, total);
        }
    }
}
