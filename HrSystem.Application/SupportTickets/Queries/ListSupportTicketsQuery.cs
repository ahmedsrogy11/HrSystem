using AutoMapper;
using HrSystem.Application.SupportTickets.Abstractions;
using HrSystem.Application.SupportTickets.Dtos;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Queries
{
    public record ListSupportTicketsQuery(
      Guid? EmployeeId,
      TicketStatus? Status,
      string? Category,
      int Page = 1,
      int PageSize = 20
    ) : IRequest<(IReadOnlyList<SupportTicketDto>, int)>;






    public class ListSupportTicketsHandler
     : IRequestHandler<ListSupportTicketsQuery, (IReadOnlyList<SupportTicketDto>, int)>
    {
        private readonly ISupportTicketRepository _repo;
        private readonly IMapper _mapper;

        public ListSupportTicketsHandler(ISupportTicketRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<SupportTicketDto>, int)> Handle(
            ListSupportTicketsQuery r,
            CancellationToken ct)
        {
            var (items, total) = await _repo.ListAsync(
                r.EmployeeId,
                r.Status,
                r.Category,
                r.Page,
                r.PageSize,
                ct
            );

            var dtos = items.Select(_mapper.Map<SupportTicketDto>).ToList();

            return (dtos, total);
        }
    }
 }
