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
    public record GetOrganizationByIdQuery(Guid Id) : IRequest<OrganizationDto?>;

    public class GetOrganizationByIdHandler : IRequestHandler<GetOrganizationByIdQuery, OrganizationDto?>
    {
        private readonly IOrganizationRepository _repo;
        private readonly IMapper _mapper;

        public GetOrganizationByIdHandler(IOrganizationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OrganizationDto?> Handle(GetOrganizationByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<OrganizationDto>(entity);
        }
    }
}
