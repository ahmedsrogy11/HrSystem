using AutoMapper;
using HrSystem.Application.Announcements.dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.Mapping
{
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>();
        }
    }
}
