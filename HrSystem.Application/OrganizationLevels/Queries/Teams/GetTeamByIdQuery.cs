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
    public record GetTeamByIdQuery(Guid Id) : IRequest<TeamDto?>;

    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamDto?>
    {
        private readonly ITeamRepository _repo;
        private readonly IMapper _mapper;

        public GetTeamByIdHandler(ITeamRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TeamDto?> Handle(GetTeamByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<TeamDto>(entity);
        }
    }
}
