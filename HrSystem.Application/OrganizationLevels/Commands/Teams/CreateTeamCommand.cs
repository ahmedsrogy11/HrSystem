using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Teams
{
    public record CreateTeamCommand(
       string Name,
       string? Code,
       Guid DepartmentId,
       Guid? LeaderEmployeeId
    ) : IRequest<TeamDto>;




    public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, TeamDto>
    {
        private readonly ITeamRepository _repo;
        private readonly IMapper _mapper;

        public CreateTeamHandler(ITeamRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TeamDto> Handle(CreateTeamCommand r, CancellationToken ct)
        {
            var entity = new Team
            {
                Name = r.Name,
                Code = r.Code,
                DepartmentId = r.DepartmentId,
                LeaderEmployeeId = r.LeaderEmployeeId
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<TeamDto>(entity);
        }
    }

}
