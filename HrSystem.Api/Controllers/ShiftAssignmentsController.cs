using HrSystem.Application.Shifts.Commands;
using HrSystem.Application.Shifts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftAssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============================
        // Create Shift Assignment
        // POST: /api/shiftassignments
        // ============================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShiftAssignmentCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        // ============================
        // Get by ID
        // GET: /api/shiftassignments/{id}
        // ============================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetShiftAssignmentByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // ============================
        // List Assignments (with filters)
        // GET: /api/shiftassignments?employeeId=&shiftId=&dateFrom=&dateTo=&page=&pageSize=
        // ============================
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? employeeId,
            [FromQuery] Guid? shiftId,
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new ListShiftAssignmentsQuery(employeeId, shiftId, dateFrom, dateTo, page, pageSize)
            );

            return Ok(new { total, items });
        }

        // ============================
        // Update Shift Assignment
        // PUT: /api/shiftassignments/{id}
        // ============================
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateShiftAssignmentCommand cmd)
        {
            if (id != cmd.Id)
                return BadRequest("Mismatched ID.");

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }

        // ============================
        // Delete (Soft Delete)
        // DELETE: /api/shiftassignments/{id}
        // ============================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteShiftAssignmentCommand(id));
            return ok ? NoContent() : NotFound();
        }
    }
}
