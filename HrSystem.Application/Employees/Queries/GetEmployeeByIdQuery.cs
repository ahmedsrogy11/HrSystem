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
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;


    public class GetEmployeeByIdHandler(IEmployeeRepository repo, IMapper mapper) :
        IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
    {
        public async Task<EmployeeDto?> Handle
            (GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var e = await repo.GetByIdAsync(request.Id, cancellationToken);
            return e is null ? null : mapper.Map<EmployeeDto>(e);
        }
    }

}
