using HrSystem.Application.Auth.Commands;
using HrSystem.Application.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Add action methods for authentication (e.g., login, logout) here
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct)
        {

            var result = await _mediator.Send(new LoginCommand(
                dto.Email, dto.Password), ct);

            return Ok(result);




        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto dto, CancellationToken ct)
        {
            var result = await _mediator.Send(new RegisterUserCommand(
                dto.EmployeeId,
                dto.Email,
                dto.UserName,
                dto.Password), ct);

            return Ok(result);
        }
    }
}
