using AutoMapper;
using HrSystem.Application.Leaves.Dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Leaves.Mapping
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>()
                .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));
        }
    }
}
