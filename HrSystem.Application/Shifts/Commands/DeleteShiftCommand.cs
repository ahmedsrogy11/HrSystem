using HrSystem.Application.Shifts.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Shifts.Commands
{
    public record DeleteShiftCommand(Guid Id) : IRequest<bool>;



    public class DeleteShiftHandler
       : IRequestHandler<DeleteShiftCommand, bool>
    {
        private readonly IShiftRepository _repo;
        public DeleteShiftHandler(IShiftRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteShiftCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
