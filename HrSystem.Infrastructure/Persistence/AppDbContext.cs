using HrSystem.Domain.Entities;
using HrSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
     {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 👇 DbSet لكل كيان رئيسي
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();

        public DbSet<PayrollPeriod> PayrollPeriods => Set<PayrollPeriod>();
        public DbSet<Payslip> Payslips => Set<Payslip>();
        public DbSet<PayslipEarning> PayslipEarnings => Set<PayslipEarning>();
        public DbSet<PayslipDeduction> PayslipDeductions => Set<PayslipDeduction>();

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();

        public DbSet<LoanRequest> LoanRequests => Set<LoanRequest>();
        public DbSet<OvertimeRequest> OvertimeRequests => Set<OvertimeRequest>();
        public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();

        public DbSet<Shift> Shifts => Set<Shift>();
        public DbSet<ShiftAssignment> ShiftAssignments => Set<ShiftAssignment>();

        public DbSet<Announcement> Announcements => Set<Announcement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 👇 تطبيق جميع ملفات التكوين (Configurations) تلقائيًا
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // تقدر تضيف config خاص بـ AppUser لو حبيت:
            modelBuilder.Entity<AppUser>(b =>
            {
                b.ToTable("Users"); // اسم جدول اليوزرز بدل AspNetUsers

                b.HasOne(u => u.Employee)
                 .WithMany() // أو WithOne(...) لو عملت navigation في Employee
                 .HasForeignKey(u => u.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // اختيارياً تغيير أسماء جداول Identity الافتراضية:
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoleJoins");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        




            // Global Query Filter (exclude soft deleted records)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var deletedProp = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var filter = Expression.Lambda(Expression.Equal(deletedProp, Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified; // مهم جداً
                    entry.Entity.IsDeleted = true;
                    
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


     }
}
