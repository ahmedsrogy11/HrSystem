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
    public record ListBranchesQuery(
        Guid? CompanyId,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<BranchDto> Items, int Total)>;



    public class ListBranchesHandler : IRequestHandler<ListBranchesQuery, (IReadOnlyList<BranchDto>, int)>
    {
        private readonly IBranchRepository _repo;
        private readonly IMapper _mapper;
        public ListBranchesHandler(IBranchRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<(IReadOnlyList<BranchDto>, int)> Handle(ListBranchesQuery r, CancellationToken ct)
        {
            var (entities, total) =
                await _repo.ListAsync(r.CompanyId, r.Page, r.PageSize, ct);
            var dtos = entities.Select(_mapper.Map<BranchDto>).ToList();
            return (dtos, total);
        }
    }
}
