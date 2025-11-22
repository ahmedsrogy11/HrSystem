using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.Organizations.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Organizations
{
    public record CreateOrganizationCommand(
       string Name,
       string? Code
    ) : IRequest<OrganizationDto>;



    public class CreateOrganizationHandler : IRequestHandler<CreateOrganizationCommand, OrganizationDto>
    {
        private readonly IOrganizationRepository _repo;
        private readonly IMapper _mapper;

        public CreateOrganizationHandler(IOrganizationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> Handle(CreateOrganizationCommand r, CancellationToken ct)
        {
            var entity = new Organization
            {
                Name = r.Name,
                Code = r.Code
            };

            await _repo.AddAsync(entity, ct);
            return _mapper.Map<OrganizationDto>(entity);
        }
    }
}
