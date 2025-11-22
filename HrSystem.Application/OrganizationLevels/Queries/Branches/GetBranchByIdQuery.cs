using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Queries.Branches
{
    public record GetBranchByIdQuery(Guid Id) : IRequest<BranchDto?>;


    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, BranchDto?>
    {
        private readonly IBranchRepository _repo;
        private readonly IMapper _mapper;
        public GetBranchByIdHandler(IBranchRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<BranchDto?> Handle(GetBranchByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<BranchDto>(entity);
        }
    }
}
