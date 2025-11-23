using AutoMapper;
using HrSystem.Application.Announcements.Abstractions;
using HrSystem.Application.Announcements.dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.Commands
{
    public record CreateAnnouncementCommand(
       string Title,
       string Body,
       DateTime PublishAtUtc,
       bool IsGlobal
    ) : IRequest<AnnouncementDto>;




    public class CreateAnnouncementHandler
    : IRequestHandler<CreateAnnouncementCommand, AnnouncementDto>
    {
        private readonly IAnnouncementRepository _repo;
        private readonly IMapper _mapper;

        public CreateAnnouncementHandler(IAnnouncementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AnnouncementDto> Handle(CreateAnnouncementCommand r, CancellationToken ct)
        {
            var entity = new Announcement
            {
                Title = r.Title,
                Body = r.Body,
                PublishAtUtc = r.PublishAtUtc,
                IsGlobal = r.IsGlobal
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<AnnouncementDto>(entity);
        }
    }
}
