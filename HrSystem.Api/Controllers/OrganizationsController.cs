using HrSystem.Application.OrganizationLevels.Commands.Organizations;
using HrSystem.Application.OrganizationLevels.Queries.Organizations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetOrganizationByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(new ListOrganizationsQuery(page, pageSize));
            return Ok(new { total, items });
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrganizationCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("Mismatched id");

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }



        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteOrganizationCommand(id));
            return ok ? NoContent() : NotFound();
        }

    }
}
