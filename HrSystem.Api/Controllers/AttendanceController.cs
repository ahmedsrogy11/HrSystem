using HrSystem.Application.Attendance.Commands;
using HrSystem.Application.Attendance.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAttendanceRecordCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetAttendanceRecordByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? employeeId,
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new ListAttendanceRecordsQuery(employeeId, dateFrom, dateTo, page, pageSize));

            return Ok(new { total, items });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update
            (Guid id, [FromBody] UpdateAttendanceRecordCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("Mismatched id");

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteAttendanceRecordCommand(id));
            return ok ? NoContent() : NotFound();
        }



        [HttpPost("clock-in")]
        public async Task<IActionResult> ClockIn([FromBody] EmployeeClockInCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("clock-out")]
        public async Task<IActionResult> ClockOut([FromBody] EmployeeClockOutCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return result is null ? NotFound("No attendance record for today.") : Ok(result);
        }


    }
}
