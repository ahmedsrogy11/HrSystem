using HrSystem.Application.Attendance.Abstractions;
using HrSystem.Application.Employees.Abstractions;
using HrSystem.Application.Leaves.Abstractions;
using HrSystem.Application.Loans.Abstractions;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Application.Shifts.Abstractions;
using HrSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // يقرأ من appsettings.json -> ConnectionStrings:Default
            var conn = config.GetConnectionString("Default")
                     ?? "Server=AHMEDSROGY11\\MSSQLSERVER01;Database=HrSystemDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(conn));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IAttendanceRecordRepository, AttendanceRecordRepository>();
            services.AddScoped<ILoanRequestRepository, LoanRequestRepository>();
            services.AddScoped<IOvertimeRequestRepository, OvertimeRequestRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();









            return services;
        }
    }
}
