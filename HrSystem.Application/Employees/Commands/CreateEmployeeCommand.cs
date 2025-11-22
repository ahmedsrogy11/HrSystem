using AutoMapper;
using HrSystem.Application.Employees.Abstractions;
using HrSystem.Application.Employees.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Commands
{
    public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string NationalId,
    string? Email,
    string? Phone,
    string JobTitle,
    decimal BaseSalary,
    string SalaryCurrency = "EGP"
    ) : IRequest<EmployeeDto>;



    public class CreateEmployeeHandler(IEmployeeRepository repository, IMapper mapper) :
        IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (await repository.NationalIdExistsAsync(request.NationalId, cancellationToken))
                throw new InvalidOperationException("National ID already exists.");

            var e = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalId = request.NationalId,
                Email = request.Email,
                Phone = request.Phone,
                JobTitle = request.JobTitle,
                BaseSalary = request.BaseSalary,
                SalaryCurrency = request.SalaryCurrency
            };
            await repository.AddAsync(e, cancellationToken);
            return mapper.Map<EmployeeDto>(e);
        }
    }
}
