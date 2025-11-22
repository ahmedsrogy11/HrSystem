using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Dtos
{
    public record EmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    string NationalId,
    string? Email,
    string? Phone,
    string JobTitle,
    DateTime HireDate,
    decimal BaseSalary,
    string SalaryCurrency
    );
}
