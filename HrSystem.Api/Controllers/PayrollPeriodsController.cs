using HrSystem.Application.Payroll.Commands;
using HrSystem.Application.Payroll.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollPeriodsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PayrollPeriodsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // POST: api/PayrollPeriods
        [HttpPost]
        public async Task<IActionResult> Create(CreatePayrollPeriodCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }





        // GET: api/PayrollPeriods/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetPayrollPeriodByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }




        // GET: api/PayrollPeriods
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] int? year,
            [FromQuery] int? month,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] bool? isClosed,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new ListPayrollPeriodsQuery(
                    year, month, fromDate, toDate, isClosed, page, pageSize
                ));

            return Ok(new { total, items });
        }


        // PUT: api/PayrollPeriods/{id}

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdatePayrollPeriodCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("Mismatched id.");

            var ok = await _mediator.Send(cmd);
            return ok ? NoContent() : NotFound();
        }



        // DELETE: api/PayrollPeriods/{id}

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeletePayrollPeriodCommand(id));
            return ok ? NoContent() : NotFound();
        }






        [HttpPost("{id:guid}/generate-payslips")]
        public async Task<IActionResult> GeneratePayslips(Guid id)
        {
            var result = await _mediator.Send(new GeneratePayrollForPeriodCommand(id));
            return Ok(result);
        }





    }


}

