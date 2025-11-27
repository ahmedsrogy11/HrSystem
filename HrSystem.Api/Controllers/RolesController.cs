using HrSystem.Application.Common.Abstractions.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")] // هتفعلها بعد ما تضيف Role Admin
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController( IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand cmd, CancellationToken ct)
        {
           var result = await _mediator.Send(cmd, ct);

             return Ok(result);
        }


        [HttpPost]
        [Route("assign")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommand cmd, CancellationToken ct)
        {
            var result = await _mediator.Send(cmd, ct);

            return Ok(result);
        }


    }
}
