using HrSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Employee : BaseEntity
    {
        // بيانات أساسية
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string NationalId { get; set; } = default!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string JobTitle { get; set; } = default!;
        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        // حالات
        public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.Single;
        public MilitaryStatus MilitaryStatus { get; set; } = MilitaryStatus.NotApplicable;

        // الراتب الأساسي (للاستخدام في Payroll)
        public decimal BaseSalary { get; set; } = 0m;
        public string SalaryCurrency { get; set; } = "EGP";


        // الربط التنظيمي
        public Guid? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        public Guid? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public Guid? TeamId { get; set; }
        public Team? Team { get; set; }

        // Navigations العكسية
        public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
        public ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
        public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
        public ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();
        public ICollection<OvertimeRequest> OvertimeRequests { get; set; } = new List<OvertimeRequest>();
        public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
        public ICollection<ShiftAssignment> ShiftAssignments { get; set; } = new List<ShiftAssignment>();

        public ICollection<LeaveRequest> ApprovedLeaves { get; set; } = new List<LeaveRequest>();
        public ICollection<LoanRequest> ApprovedLoans { get; set; } = new List<LoanRequest>();
        public ICollection<OvertimeRequest> ApprovedOvertimes { get; set; } = new List<OvertimeRequest>();





    }
}
