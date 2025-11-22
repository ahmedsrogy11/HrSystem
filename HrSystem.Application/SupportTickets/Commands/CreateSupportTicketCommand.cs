using AutoMapper;
using HrSystem.Application.SupportTickets.Abstractions;
using HrSystem.Application.SupportTickets.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets.Commands
{
    public record CreateSupportTicketCommand(
         Guid EmployeeId,
         string Title,
         string Description,
         string? Category
    ) : IRequest<SupportTicketDto>;





    public class CreateSupportTicketHandler
        : IRequestHandler<CreateSupportTicketCommand, SupportTicketDto>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _repo;

        public CreateSupportTicketHandler(IMapper mapper , ISupportTicketRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<SupportTicketDto> Handle(CreateSupportTicketCommand r, CancellationToken ct)
        {
            var entity = new SupportTicket
            {
                EmployeeId = r.EmployeeId,
                Title = r.Title,
                Description = r.Description,
                Category = r.Category,
            };


            await _repo.AddAsync(entity, ct);
            return _mapper.Map<SupportTicketDto>(entity);


        }
    }
}
