using AutoMapper;
using HrSystem.Application.Announcements.Abstractions;
using HrSystem.Application.Announcements.dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.Queries
{
    public record GetAnnouncementByIdQuery(Guid Id) : IRequest<AnnouncementDto?>;



    public class GetAnnouncementByIdHandler
    : IRequestHandler<GetAnnouncementByIdQuery, AnnouncementDto?>
    {
        private readonly IAnnouncementRepository _repo;
        private readonly IMapper _mapper;

        public GetAnnouncementByIdHandler(IAnnouncementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AnnouncementDto?> Handle(GetAnnouncementByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<AnnouncementDto>(entity);
        }
    }
}
