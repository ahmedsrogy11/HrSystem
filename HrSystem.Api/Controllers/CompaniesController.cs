using HrSystem.Application.OrganizationLevels.Commands.Companies;
using HrSystem.Application.OrganizationLevels.Queries.Companies;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCompanyByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? organizationId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) =
                await _mediator.Send(new ListCompaniesQuery(organizationId, page, pageSize));

            return Ok(new { total, items });
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("Mismatched id");

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }




        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteCompanyCommand(id));
            return ok ? NoContent() : NotFound();
        }
    }
}
