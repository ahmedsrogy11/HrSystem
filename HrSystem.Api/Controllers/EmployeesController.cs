using HrSystem.Application.Employees.Commands;
using HrSystem.Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }




        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("Mismatched ID");
            var success= await mediator.Send(cmd);
            return success ? NoContent() : NotFound();
        }




        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await mediator.Send(new DeleteEmployeeCommand(id));
            return success ? NoContent() : NotFound();
        }




        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var (items, total) = await mediator.Send(new ListEmployeesQuery(page, pageSize));
            return Ok(new { total, items });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await mediator.Send(new GetEmployeeByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }
    }
}
