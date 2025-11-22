using AutoMapper;
using HrSystem.Application.OrganizationLevels.Dtos;
using HrSystem.Application.Organizations.Dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Organizations.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<Branch, BranchDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Team, TeamDto>();
        }
    }
}
