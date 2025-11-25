using HrSystem.Application.Payroll.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayslipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PayslipsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        // GET: api/Payslips/{id}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPayslipByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }



        // GET: api/Payslips/by-period/{periodId}

        [HttpGet("by-period/{periodId:guid}")]
        public async Task<IActionResult> GetByPeriod(
            Guid periodId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new GetPayslipsByPeriodQuery(periodId, page, pageSize));

            return Ok(new { total, items });
        }


        // GET: api/Payslips/by-employee/{employeeId}

        [HttpGet("by-employee/{employeeId:guid}")]
        public async Task<IActionResult> GetByEmployee(
            Guid employeeId,
            [FromQuery] Guid? periodId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _mediator.Send(
                new GetPayslipsByEmployeeQuery(employeeId, periodId, page, pageSize));

            return Ok(new { total, items });
        }
    }
}
