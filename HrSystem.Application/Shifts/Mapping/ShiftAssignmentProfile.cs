using AutoMapper;
using HrSystem.Application.Shifts.Dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Mapping
{
    public class ShiftAssignmentProfile : Profile
    {
        public ShiftAssignmentProfile()
        {
            CreateMap<ShiftAssignment, ShiftAssignmentDto>();
        }
    }
}
