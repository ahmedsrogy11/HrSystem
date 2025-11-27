using HrSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Persistence
{
    public class OrganizationConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> b)
        {
            b.ToTable("Organizations");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();

            b.HasMany(x => x.Companies)
             .WithOne(x => x.Organization)
             .HasForeignKey(x => x.OrganizationId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }

    // CompanyConfig.cs
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> b)
        {
            b.ToTable("Companies");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();

            b.HasMany(x => x.Branches)
             .WithOne(x => x.Company)
             .HasForeignKey(x => x.CompanyId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }

    // BranchConfig.cs
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> b)
        {
            b.ToTable("Branches");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();

            b.HasMany(x => x.Departments)
             .WithOne(x => x.Branch)
             .HasForeignKey(x => x.BranchId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }

    // DepartmentConfig.cs
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> b)
        {
            b.ToTable("Departments");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();

            b.HasMany(x => x.Teams)
             .WithOne(x => x.Department)
             .HasForeignKey(x => x.DepartmentId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> b)
        {
            b.ToTable("Teams");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();

        
            b.HasOne(x => x.LeaderEmployee)
             .WithMany() // مفيش قائمة عكسية للقادة
             .HasForeignKey(x => x.LeaderEmployeeId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(x => x.Employees)
             .WithOne(x => x.Team)
             .HasForeignKey(x => x.TeamId)
             .OnDelete(DeleteBehavior.SetNull);
        }
    }

    // EmployeeConfig.cs
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> b)
        {
            b.ToTable("Employees");
            b.HasKey(x => x.Id);

            b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            b.Property(x => x.NationalId).HasMaxLength(20).IsRequired();
            b.Property(x => x.JobTitle).HasMaxLength(120).IsRequired();
            b.Property(x => x.Email).HasMaxLength(200);

            // 💰 إضافة الدقة للراتب الأساسي
            b.Property(x => x.BaseSalary).HasPrecision(18, 2);


            b.HasIndex(x => x.NationalId).IsUnique();

            // لو حاب تخلي الإيميل إجباري للدخول:
            b.HasIndex(x => x.Email).IsUnique();

            // ربطات اختيارية للفلترة
            b.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).OnDelete(DeleteBehavior.SetNull);
            b.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.SetNull);
            b.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            b.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.SetNull);
            b.HasOne(x => x.Team).WithMany(t => t.Employees).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.SetNull);

            // Navigations العكسية مبيّنة تلقائيًا بالباقي
        }
    }

    // AttendanceRecordConfig.cs
    public class AttendanceRecordConfig : IEntityTypeConfiguration<AttendanceRecord>
    {
        public void Configure(EntityTypeBuilder<AttendanceRecord> b)
        {
            b.ToTable("AttendanceRecords");
            b.HasKey(x => x.Id);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.AttendanceRecords)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.NoAction); // لا نحذف سجلات الحضور
        }
    }
    //laveRequestConfig.cs
    public class LeaveRequestConfig : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> b)
        {
            b.ToTable("LeaveRequests");
            b.HasKey(x => x.Id);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.LeaveRequests)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.ApprovedByEmployee)
            .WithMany(e => e.ApprovedLeaves)   // أو .WithMany() لو مش هتضيف القائمة في Employee
            .HasForeignKey(x => x.ApprovedByEmployeeId)
             .OnDelete(DeleteBehavior.SetNull);

        }
    }

    // LeaveBalanceConfig.cs
    public class LeaveBalanceConfig : IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> b)
        {
            b.ToTable("LeaveBalances");
            b.HasKey(x => x.Id);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.LeaveBalances)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.NoAction); // حذف الموظف يحذف رصيده (قرار تصميمي)

            b.HasIndex(x => new { x.EmployeeId }).IsUnique(false); // يمكنك جعله Unique إذا واحد فقط سنويًا
        }
    }

    // PayrollPeriodConfig.cs
    public class PayrollPeriodConfig : IEntityTypeConfiguration<PayrollPeriod>
    {
        public void Configure(EntityTypeBuilder<PayrollPeriod> b)
        {
            b.ToTable("PayrollPeriods");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.Year, x.Month }).IsUnique();

            b.HasMany(x => x.Payslips)
             .WithOne(x => x.PayrollPeriod)
             .HasForeignKey(x => x.PayrollPeriodId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }

    // PayslipConfig.cs
    public class PayslipConfig : IEntityTypeConfiguration<Payslip>
    {
        public void Configure(EntityTypeBuilder<Payslip> b)
        {
            b.ToTable("Payslips");
            b.HasKey(x => x.Id);
            // 💰 إضافة الدقة لخصائص الحسابات في Payslip
            b.Property(x => x.GrossEarnings).HasPrecision(18, 2);
            b.Property(x => x.NetPay).HasPrecision(18, 2);
            b.Property(x => x.TotalDeductions).HasPrecision(18, 2);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.Payslips)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.NoAction);

            b.HasMany(x => x.Earnings)
             .WithOne(e => e.Payslip)
             .HasForeignKey(e => e.PayslipId)
             .OnDelete(DeleteBehavior.NoAction);

            b.HasMany(x => x.Deductions)
             .WithOne(d => d.Payslip)
             .HasForeignKey(d => d.PayslipId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }

    // PayslipEarningConfig.cs
    public class PayslipEarningConfig : IEntityTypeConfiguration<PayslipEarning>
    {
        public void Configure(EntityTypeBuilder<PayslipEarning> b)
        {
            b.ToTable("PayslipEarnings");
            b.HasKey(x => x.Id);
            // 💰 إضافة الدقة لمبلغ الاستحقاق
            b.Property(x => x.Amount).HasPrecision(18, 2);
        }
    }

    // PayslipDeductionConfig.cs
    public class PayslipDeductionConfig : IEntityTypeConfiguration<PayslipDeduction>
    {
        public void Configure(EntityTypeBuilder<PayslipDeduction> b)
        {
            b.ToTable("PayslipDeductions");
            b.HasKey(x => x.Id);
            // 💰 إضافة الدقة لمبلغ الاستقطاع
            b.Property(x => x.Amount).HasPrecision(18, 2);

        }
    }


    // RoleConfig.cs
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.ToTable("Roles");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(150).IsRequired();

            b.HasMany(x => x.RolePermissions)
             .WithOne(x => x.Role)
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.EmployeeRoles)
             .WithOne(x => x.Role)
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }

    // PermissionConfig.cs
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> b)
        {
            b.ToTable("Permissions");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(200).IsRequired();
            b.HasIndex(x => x.Code).IsUnique();

            b.HasMany(x => x.RolePermissions)
             .WithOne(x => x.Permission)
             .HasForeignKey(x => x.PermissionId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }

    // RolePermissionConfig.cs
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> b)
        {
            b.ToTable("RolePermissions");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();
        }
    }

    // EmployeeRoleConfig.cs
    public class EmployeeRoleConfig : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> b)
        {
            b.ToTable("EmployeeRoles");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.EmployeeId, x.RoleId }).IsUnique();

            b.HasOne(x => x.Employee)
             .WithMany(e => e.EmployeeRoles)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
    // LoanRequestConfig.cs
    public class LoanRequestConfig : IEntityTypeConfiguration<LoanRequest>
    {
        public void Configure(EntityTypeBuilder<LoanRequest> b)
        {
            b.ToTable("LoanRequests");
            b.HasKey(x => x.Id);
            // 💰 إضافة الدقة لمبلغ القرض
            b.Property(x => x.Amount).HasPrecision(18, 2);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.LoanRequests)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ApprovedByEmployee)
             .WithMany(e => e.ApprovedLoans)
             .HasForeignKey(x => x.ApprovedByEmployeeId)
             .OnDelete(DeleteBehavior.SetNull);
        }
    }

    // OvertimeRequestConfig.cs
    public class OvertimeRequestConfig : IEntityTypeConfiguration<OvertimeRequest>
    {
        public void Configure(EntityTypeBuilder<OvertimeRequest> b)
        {
            b.ToTable("OvertimeRequests");
            b.HasKey(x => x.Id);
            // ⏱️ إضافة الدقة للساعات الإضافية
            b.Property(x => x.Hours).HasPrecision(10, 4);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.OvertimeRequests)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ApprovedByEmployee)
             .WithMany(e => e.ApprovedOvertimes)
             .HasForeignKey(x => x.ApprovedByEmployeeId)
             .OnDelete(DeleteBehavior.SetNull);
        }
    }

    // SupportTicketConfig.cs
    public class SupportTicketConfig : IEntityTypeConfiguration<SupportTicket>
    {
        public void Configure(EntityTypeBuilder<SupportTicket> b)
        {
            b.ToTable("SupportTickets");
            b.HasKey(x => x.Id);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.SupportTickets)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }

    // ShiftConfig.cs
    public class ShiftConfig : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> b)
        {
            b.ToTable("Shifts");
            b.HasKey(x => x.Id);

            b.HasMany(x => x.Assignments)
             .WithOne(x => x.Shift)
             .HasForeignKey(x => x.ShiftId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }

    // ShiftAssignmentConfig.cs
    public class ShiftAssignmentConfig : IEntityTypeConfiguration<ShiftAssignment>
    {
        public void Configure(EntityTypeBuilder<ShiftAssignment> b)
        {
            b.ToTable("ShiftAssignments");
            b.HasKey(x => x.Id);

            b.HasOne(x => x.Employee)
             .WithMany(e => e.ShiftAssignments)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }

    // AnnouncementConfig.cs
    public class AnnouncementConfig : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> b)
        {
            b.ToTable("Announcements");
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).HasMaxLength(200).IsRequired();
        }
    }

}
