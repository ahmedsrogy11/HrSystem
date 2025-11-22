using HrSystem.Application.SupportTickets.Commands;
using HrSystem.Application.SupportTickets.Queries;
using HrSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupportTicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateSupportTicketCommand cmd)
            => Ok(await _mediator.Send(cmd));





        // Get By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetSupportTicketByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }


        // List
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? employeeId,
            [FromQuery] TicketStatus? status,
            [FromQuery] string? category,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _mediator
                .Send(new ListSupportTicketsQuery(employeeId, status, category, page, pageSize));

            return Ok(result);

        }










        // Update
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateSupportTicketCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : BadRequest();
        }


        // Delete - Soft Delete
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteSupportTicketCommand(id));
            return ok ? NoContent() : BadRequest();
        }

    }
}
