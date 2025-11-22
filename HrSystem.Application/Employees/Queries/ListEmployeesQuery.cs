using AutoMapper;
using HrSystem.Application.Employees.Abstractions;
using HrSystem.Application.Employees.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Queries
{
    public record ListEmployeesQuery(int Page = 1, int PageSize = 20)
    : IRequest<(IReadOnlyList<EmployeeDto> Items, int Total)>;


    public class ListEmployeesHandler(IEmployeeRepository repo, IMapper mapper)
   : IRequestHandler<ListEmployeesQuery, (IReadOnlyList<EmployeeDto>, int)>
    {
        public async Task<(IReadOnlyList<EmployeeDto>, int)> Handle(ListEmployeesQuery r, CancellationToken ct)
        {
            var (entities, total) = await repo.ListAsync(r.Page, r.PageSize, ct);
            var dtos = entities.Select(mapper.Map<EmployeeDto>).ToList();
            return (dtos, total);
        }
    }
}
