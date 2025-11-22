using AutoMapper;
using HrSystem.Application.Loans.Dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.mapping
{
    public class LoanRequestProfile : Profile
    {
        public LoanRequestProfile()
        {
            CreateMap<LoanRequest, LoanRequestDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        }
    }
}
