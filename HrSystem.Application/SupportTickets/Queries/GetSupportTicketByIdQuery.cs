using AutoMapper;
using HrSystem.Application.SupportTickets.Abstractions;
using HrSystem.Application.SupportTickets.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Queries
{
    public record GetSupportTicketByIdQuery(Guid Id) : IRequest<SupportTicketDto?>;








    public class GetSupportTicketByIdHandler : IRequestHandler<GetSupportTicketByIdQuery, SupportTicketDto?>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _repo;

        public GetSupportTicketByIdHandler(IMapper mapper, ISupportTicketRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<SupportTicketDto?> Handle(GetSupportTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            return entity == null ? null : _mapper.Map<SupportTicketDto>(entity);
        }
    }
}
