using AutoMapper;
using HrSystem.Application.Attendance.Abstractions;
using HrSystem.Application.Employees.Abstractions;
using HrSystem.Application.Loans.Abstractions;
using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Application.Payroll.Abstractions;
using HrSystem.Application.Payroll.Dtos;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Commands
{
    public record GeneratePayrollForPeriodCommand(Guid PayrollPeriodId)
       : IRequest<IReadOnlyList<PayslipDto>>;




    public class GeneratePayrollForPeriodHandler
         : IRequestHandler<GeneratePayrollForPeriodCommand, IReadOnlyList<PayslipDto>>
    {
        private readonly IPayrollPeriodRepository _periods;
        private readonly IEmployeeRepository _employees;
        private readonly IAttendanceRecordRepository _attendance;
        private readonly IOvertimeRequestRepository _overtime;
        private readonly ILoanRequestRepository _loans;
        private readonly IPayslipRepository _payslips;
        private readonly IMapper _mapper;

        // نسب مؤقتة – يمكنك نقلها مستقبلاً إلى إعدادات أو جدول Config
        private const decimal TaxRate = 0.10m;              // 10% ضريبة
        private const decimal SocialInsuranceRate = 0.11m;  // 11% تأمينات
        private const decimal OvertimeMultiplier = 1.5m;    // ساعة أوفر = 1.5 من الساعة العادية

        public GeneratePayrollForPeriodHandler(
            IPayrollPeriodRepository periods,
            IEmployeeRepository employees,
            IAttendanceRecordRepository attendance,
            IOvertimeRequestRepository overtime,
            ILoanRequestRepository loans,
            IPayslipRepository payslips,
            IMapper mapper)
        {
            _periods = periods;
            _employees = employees;
            _attendance = attendance;
            _overtime = overtime;
            _loans = loans;
            _payslips = payslips;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<PayslipDto>> Handle(GeneratePayrollForPeriodCommand r, CancellationToken ct)
        {
            // جلب فترة الرواتب
            var period = await _periods.GetByIdAsync(r.PayrollPeriodId, ct)
                ?? throw new InvalidOperationException("Payroll period not found.");

            var fromDate = period.FromDate;
            var toDate = period.ToDate;

            //p حذف الرواتب القديمة للفترة
            await _payslips.DeleteByPeriodAsync(r.PayrollPeriodId, ct);



            // 3) جلب كل الموظفين (نستخدم pageSize كبير كـ workaround)
            var (employeeItems, _) = await _employees.ListAsync(
                  page: 1,
                  pageSize: int.MaxValue,
                  ct );

            var employees = employeeItems
               .Where(e => !e.IsDeleted && e.BaseSalary > 0)
               .ToList();



            //  إنشاء قسائم الرواتب لكل موظف
            var payslips = new List<Payslip>();

            foreach (var emp in employeeItems)
            {
                // حسابات الرواتب
                var baseSalary = emp.BaseSalary;



                // 4) حساب أجر اليوم والساعة
                var dailyRate = baseSalary / 30m;
                var hourlyRate = dailyRate / 8m;


                // 5) أوفر تايم Approved// 5) حساب ساعات الأوفر تايم من OvertimeRequests
                var (overtimeItems, total) = await _overtime.ListAsync(
                    employeeId: emp.Id,
                    status: OvertimeStatus.Approved,
                    dateFrom: fromDate,
                    dateTo: toDate,
                    page: 1,
                    pageSize: int.MaxValue,
                    ct );

                var totalOvertimeHours = overtimeItems
                    .Sum(o => o.Hours);

                var overtimeAmount = totalOvertimeHours * hourlyRate * OvertimeMultiplier;



                // 6) الغياب من Attendance
                var (attendanceItems, _) = await _attendance.ListAsync(
                    employeeId: emp.Id,
                    dateFrom: fromDate,
                    dateTo: toDate,
                    page: 1,
                    pageSize: int.MaxValue,
                    ct: ct);

                var absenceDays = attendanceItems.Count(a => a.IsAbsent);
                var absenceAmount = absenceDays * dailyRate;


                // 7) قروض Approved
                var (loanItems, _) = await _loans.ListAsync(
                    employeeId: emp.Id,
                    status: LoanStatus.Approved,
                    page: 1,
                    pageSize: int.MaxValue,
                    ct: ct);

                decimal loanInstallments = 0m;
                foreach (var loan in loanItems)
                {
                    if (loan.Months > 0)
                        loanInstallments += loan.Amount / loan.Months;
                }


                // 8) ضرائب وتأمينات
                var taxableBase = baseSalary + overtimeAmount;
                var taxAmount = taxableBase * TaxRate;
                var socialInsuranceAmount = baseSalary * SocialInsuranceRate;


                // 9) Build Earnings
                var earnings = new List<PayslipEarning>
                {
                    new PayslipEarning
                    {
                        Type = EarningType.BaseSalary,
                        Amount = baseSalary,
                        Notes = "Basic monthly salary"
                    }
                };

                if (overtimeAmount > 0)
                {
                    earnings.Add(new PayslipEarning
                    {
                        Type = EarningType.Overtime,
                        Amount = overtimeAmount,
                        Notes = $"Overtime hours: {totalOvertimeHours}"
                    });

                }

                // 10) Build Deductions
                var deductions = new List<PayslipDeduction>();

                if (absenceAmount > 0)
                {
                    deductions.Add(new PayslipDeduction
                    {
                        Type = DeductionType.Absence,
                        Amount = absenceAmount,
                        Notes = $"Absence days: {absenceDays}"
                    });
                }

                if (loanInstallments > 0)
                {
                    deductions.Add(new PayslipDeduction
                    {
                        Type = DeductionType.LoanInstallment,
                        Amount = loanInstallments,
                        Notes = "Loan installments for this month"
                    });
                }

                if (taxAmount > 0)
                {
                    deductions.Add(new PayslipDeduction
                    {
                        Type = DeductionType.Tax,
                        Amount = taxAmount,
                        Notes = $"Tax ({TaxRate:P0})"
                    });
                }

                if (socialInsuranceAmount > 0)
                {
                    deductions.Add(new PayslipDeduction
                    {
                        Type = DeductionType.SocialInsurance,
                        Amount = socialInsuranceAmount,
                        Notes = $"Social insurance ({SocialInsuranceRate:P0})"
                    });
                }


                // 11) حساب الإجماليات
                var gross = earnings.Sum(e => e.Amount);
                var totalDed = deductions.Sum(d => d.Amount);
                var net = gross - totalDed;

                var payslip = new Payslip
                {
                    EmployeeId = emp.Id,
                    PayrollPeriodId = period.Id,
                    GrossEarnings = gross,
                    TotalDeductions = totalDed,
                    NetPay = net,
                    Earnings = earnings,
                    Deductions = deductions
                };

                payslips.Add(payslip);
            }

            // 12) تخزين كل الـ Payslips مرة واحدة
            await _payslips.AddRangeAsync(payslips, ct);

            // 13) تحويلها لـ DTO
            var dtos = payslips
                .Select(p => _mapper.Map<PayslipDto>(p))
                .ToList();

            return dtos;


        }

    }

}
