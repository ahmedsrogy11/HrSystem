using AutoMapper;
using HrSystem.Application.SupportTickets.Dtos;
using HrSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Mapping
{
    public class SupportTicketProfile : Profile
    {
        public SupportTicketProfile()
        {
            CreateMap<SupportTicket, SupportTicketDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        }
    }
}
