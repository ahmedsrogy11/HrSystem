using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Queries.Companies
{
    public record ListCompaniesQuery(
     Guid? OrganizationId,
     int Page = 1,
     int PageSize = 20
    ) : IRequest<(IReadOnlyList<CompanyDto> Items, int Total)>;


    public class ListCompaniesHandler : IRequestHandler<ListCompaniesQuery, (IReadOnlyList<CompanyDto>, int)>
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;

        public ListCompaniesHandler(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<CompanyDto>, int)> Handle(ListCompaniesQuery r, CancellationToken ct)
        {
            var (entities, total) =
                await _repo.ListAsync(r.OrganizationId, r.Page, r.PageSize, ct);

            var dtos = entities.Select(_mapper.Map<CompanyDto>).ToList();

            return (dtos, total);
        }
    }
}
