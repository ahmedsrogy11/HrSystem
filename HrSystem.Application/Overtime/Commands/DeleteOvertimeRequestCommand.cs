using HrSystem.Application.Overtime.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Commands
{
    public record DeleteOvertimeRequestCommand(Guid Id) : IRequest<bool>;







    public class DeleteOvertimeRequestHandler
       : IRequestHandler<DeleteOvertimeRequestCommand, bool>
    {
        private readonly IOvertimeRequestRepository _repo;

        public DeleteOvertimeRequestHandler(IOvertimeRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteOvertimeRequestCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
