using HrSystem.Application.Overtime.Commands;
using HrSystem.Application.Overtime.Queries;
using HrSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OvertimeRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // =======================
        // Create Overtime Request
        // =======================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOvertimeRequestCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        // =============================
        // Approve Overtime Request
        // =============================
        [HttpPost("{id:guid}/approve")]
        public async Task<IActionResult> Approve
            (Guid id, [FromQuery] Guid approvedByEmployeeId)
        {
            var result = await _mediator.Send(
                new ApproveOvertimeRequestCommand(id, approvedByEmployeeId));

            return result ? Ok("Approved") : NotFound();
        }

        // =============================
        // Get By Id
        // =============================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetOvertimeRequestByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // =============================
        // List Overtime Requests
        // =============================
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? employeeId,
            [FromQuery] OvertimeStatus? status,
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new ListOvertimeRequestsQuery(employeeId, status, dateFrom, dateTo, page, pageSize));

            return Ok(new { total, items });
        }

        // =============================
        // Soft Delete
        // =============================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteOvertimeRequestCommand(id));
            return ok ? NoContent() : NotFound();
        }
    }
}
