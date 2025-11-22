using HrSystem.Application.Shifts.Commands;
using HrSystem.Application.Shifts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShiftCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetShiftByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }




        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _mediator.Send(new ListShiftsQuery()));
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateShiftCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("Mismatched id");

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }




        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteShiftCommand(id));
            return ok ? NoContent() : NotFound();
        }
    }
}

