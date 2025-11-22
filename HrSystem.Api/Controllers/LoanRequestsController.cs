using HrSystem.Application.Loans.Commands;
using HrSystem.Application.Loans.Queries;
using HrSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LoanRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanRequestCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }






        [HttpPost("{id:guid}/approve")]
        public async Task<IActionResult> Approve(Guid id, Guid approvedByEmployeeId)
        {
            var ok = await _mediator.Send(new ApproveLoanRequestCommand(id, approvedByEmployeeId));
            return ok ? Ok() : NotFound();
        }






        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] Guid? employeeId,
            [FromQuery] LoanStatus? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) =
                await _mediator
                .Send(new ListLoanRequestsQuery(employeeId, status, page, pageSize));

            return Ok(new { total, items });
        }






        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteLoanRequestCommand(id));
            return NoContent();
        }





        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLoanRequestByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }


    }
}
