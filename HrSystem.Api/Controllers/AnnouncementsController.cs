using HrSystem.Application.Announcements.Commands;
using HrSystem.Application.Announcements.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnnouncementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementCommand cmd)
            => Ok(await _mediator.Send(cmd));






        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetAnnouncementByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }





        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] bool? isGlobal,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new ListAnnouncementsQuery(dateFrom, dateTo, isGlobal, page, pageSize));

            return Ok(new { total, items });
        }





        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateAnnouncementCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }






        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteAnnouncementCommand(id));
            return ok ? NoContent() : NotFound();
        }
    }
}

