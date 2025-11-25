using AutoMapper;
using HrSystem.Application.Payroll.Dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Payroll.Mapping
{
    public class PayrollPeriodProfile : Profile
    {
        public PayrollPeriodProfile()
        {
            CreateMap<PayrollPeriod, PayrollPeriodDto>()
                .ForMember(d => d.PayslipCount, opt => opt.MapFrom(s => s.Payslips.Count));
        }
    }


    public class PayslipProfile : Profile
    {
        public PayslipProfile()
        {
            CreateMap<Payslip, PayslipDto>()
                .ForMember(d => d.EmployeeName,
                    opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.Earnings,
                    opt => opt.MapFrom(s => s.Earnings))
                .ForMember(d => d.Deductions,
                    opt => opt.MapFrom(s => s.Deductions));

            CreateMap<PayslipEarning, PayslipEarningItemDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()));

            CreateMap<PayslipDeduction, PayslipDeductionItemDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()));
        }
    }
}
