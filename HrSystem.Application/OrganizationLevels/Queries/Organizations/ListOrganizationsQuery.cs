using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.Organizations.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Queries.Organizations
{
    public record ListOrganizationsQuery(
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<OrganizationDto> Items, int Total)>;


    public class ListOrganizationsHandler : IRequestHandler<ListOrganizationsQuery, (IReadOnlyList<OrganizationDto>, int)>
    {
        private readonly IOrganizationRepository _repo;
        private readonly IMapper _mapper;

        public ListOrganizationsHandler(IOrganizationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<OrganizationDto>, int)> Handle(ListOrganizationsQuery r, CancellationToken ct)
        {
            var (entities, total) = await _repo.ListAsync(r.Page, r.PageSize, ct);
            var dtos = entities.Select(_mapper.Map<OrganizationDto>).ToList();
            return (dtos, total);
        }
    }
}
