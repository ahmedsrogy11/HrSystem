using HrSystem.Application.Shifts.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Commands
{
    public record DeleteShiftAssignmentCommand(Guid Id) : IRequest<bool>;





    public class DeleteShiftAssignmentHandler
       : IRequestHandler<DeleteShiftAssignmentCommand, bool>
    {
        // Assume _repo is injected via constructor (not shown for brevity)
        private readonly IShiftAssignmentRepository _repo;
        public DeleteShiftAssignmentHandler(IShiftAssignmentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteShiftAssignmentCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
