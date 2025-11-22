using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Queries.Departments
{
    public record ListDepartmentsQuery(
        Guid? BranchId,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<DepartmentDto> Items, int Total)>;



    public class ListDepartmentsHandler : IRequestHandler<ListDepartmentsQuery, (IReadOnlyList<DepartmentDto>, int)>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public ListDepartmentsHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<DepartmentDto>, int)> Handle(ListDepartmentsQuery r, CancellationToken ct)
        {
            var (entities, total) =
                await _repo.ListAsync(r.BranchId, r.Page, r.PageSize, ct);

            var dtos = entities.Select(_mapper.Map<DepartmentDto>).ToList();
            return (dtos, total);
        }
    }
}
