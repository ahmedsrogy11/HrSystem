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
    public record ListAnnouncementsQuery(
        DateTime? DateFrom,
        DateTime? DateTo,
        bool? IsGlobal,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<(IReadOnlyList<AnnouncementDto>, int)>;



    public class ListAnnouncementsHandler
    : IRequestHandler<ListAnnouncementsQuery, (IReadOnlyList<AnnouncementDto>, int)>
    {
        private readonly IAnnouncementRepository _repo;
        private readonly IMapper _mapper;

        public ListAnnouncementsHandler(IAnnouncementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<AnnouncementDto>, int)> Handle(
            ListAnnouncementsQuery r,
            CancellationToken ct)
        {
            var (items, total) = await _repo.ListAsync(
                r.DateFrom, r.DateTo, r.IsGlobal,
                r.Page, r.PageSize, ct
            );

            var dtos = items.Select(_mapper.Map<AnnouncementDto>).ToList();

            return (dtos, total);
        }
    }
}
