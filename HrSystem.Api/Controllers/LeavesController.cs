using HrSystem.Application.Leaves.Commands;
using HrSystem.Application.Leaves.Queries;
using HrSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController (IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaveRequestCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLeaveRequestByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? employeeId,
            [FromQuery] LeaveStatus? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
             var (items, total) = await _mediator.Send(
                new ListLeaveRequestsQuery(employeeId, status, page, pageSize));
            return Ok(new { total, items });
        }


        [HttpPut("{id:guid}/approve")]
        public async Task<IActionResult> Approve(Guid id, [FromBody] Guid approverEmployeeId)
        {
            var success = await _mediator.Send(
                new ApproveLeaveRequestCommand(id, approverEmployeeId));

            return success ? NoContent() : NotFound();
        }



        [HttpPut("{id:guid}/reject")]
        public async Task<IActionResult> Reject(Guid id, [FromBody] RejectLeaveRequestCommand body )
        {
            if (id != body.Id) return BadRequest("Mismatched id");
            var success = await _mediator.Send(body);
            return success ? NoContent() : NotFound();
        }
    }
}
