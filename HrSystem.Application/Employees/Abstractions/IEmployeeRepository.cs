using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Employees.Abstractions
{
    public interface IEmployeeRepository
    {
        // عمليات قراءة
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<bool> NationalIdExistsAsync(string nationalId, CancellationToken ct);
        Task<(IReadOnlyList<Employee> Items, int Total)>ListAsync
            (int page, int pageSize, CancellationToken ct);

        // عمليات تعديل
        Task AddAsync(Employee employee, CancellationToken ct);
        Task UpdateAsync(Employee employee, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);

        Task<Employee?> GetByEmailAsync(string email, CancellationToken ct);
    }

}
