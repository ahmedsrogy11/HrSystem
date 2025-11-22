using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Queries.Teams
{
    public record ListTeamsQuery(
       Guid? DepartmentId,
       int Page = 1,
       int PageSize = 20
    ) : IRequest<(IReadOnlyList<TeamDto> Items, int Total)>;




    public class ListTeamsHandler : IRequestHandler<ListTeamsQuery, (IReadOnlyList<TeamDto>, int)>
    {
        private readonly ITeamRepository _repo;
        private readonly IMapper _mapper;

        public ListTeamsHandler(ITeamRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<TeamDto>, int)> Handle(ListTeamsQuery r, CancellationToken ct)
        {
            var (entities, total) =
                await _repo.ListAsync(r.DepartmentId, r.Page, r.PageSize, ct);

            var dtos = entities.Select(_mapper.Map<TeamDto>).ToList();

            return (dtos, total);
        }
    }
}
