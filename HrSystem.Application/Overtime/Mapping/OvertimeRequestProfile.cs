using AutoMapper;
using HrSystem.Application.Overtime.Dto;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Mapping
{
    public class OvertimeRequestProfile : Profile
    {
        public OvertimeRequestProfile()
        {
            CreateMap<OvertimeRequest, OvertimeRequestDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        }
    }
}
